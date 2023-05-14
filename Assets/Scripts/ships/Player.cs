using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Ship
{
    GameState gameState;
    public GameObject gameAreaGameObject;
    Renderer gameAreaRenderer;
    public GameObject pauseMenuGameObject;
    public GameObject cheatsMenuGameObject;
    public GameObject gameOverMenuGameObject;
    public GameObject trailsMenuGameObject;
    TrailsMenu trailsMenu;
    public UnityEvent<int> onLifeChange;
    public UnityEvent<int> onShieldChange;
    public UnityEvent onTrailsChange;

    // Powerups
    int weaponIsBuffedCount = 0;
    float weaponBuffDuration = 5f;
    int weaponBuffDamage = 2;

    // Updates
    public int trailsSum { get { return missileTrail + plasmaTrail + laserTrail; } }
    public int plasmaTrail = 0;
    int plasmaBuffDamage = 1;
    bool plasmaBuffApplied = false;
    public int missileTrail = 0;
    int missileBuffLife = 1;
    bool missileBuffApplied = false;
    public int laserTrail = 0;
    float laserTrailBuffSpeed = 3f;
    bool laserBuffApplied = false;
    bool markerIsOn = false;

    List<Vector3> previousPositions = new List<Vector3>();
    public GameObject markerGameObject;

    public GameObject plasmaExplosionPrefab;

    public GameObject missileCompanionPrefab;
    GameObject missileCompanionGameObject;

    int abilityCooldown = 1;
    bool abilityIsOnCooldown = false;

    GameObject primaryWeaponCannonPrefab;
    Transform primaryCannonLocation;

    public ProjectileType primaryWeapon = ProjectileType.BULLET;

    [HideInInspector]
    public Cannon primaryCannon;
    public GameObject bulletCannonPrefab;
    public GameObject laserCannonPrefab;
    public GameObject plasmaCannonPrefab;
    public GameObject missileCannonPrefab;

    public UnityEvent onStateLoaded;

    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        gameState.LoadPlayerData(this);
        onStateLoaded.Invoke();

        gameAreaRenderer = gameAreaGameObject.GetComponent<Renderer>();
        pauseMenuGameObject.SetActive(false);
        cheatsMenuGameObject.SetActive(false);
        gameOverMenuGameObject.SetActive(false);
        trailsMenuGameObject.SetActive(false);
        trailsMenu = trailsMenuGameObject.GetComponent<TrailsMenu>();

        Time.timeScale = 1;

        var plasmaExplosion = plasmaExplosionPrefab.GetComponent<PlasmaExplosionPower>();
        plasmaExplosion.DefineTeam(team);

        rb = gameObject.GetComponent<Rigidbody>();
        team = Team.PLAYER;
        life = 3;
        maxLife = 5;

        markerGameObject.SetActive(false);


        CheckTrails();
        BuildShip();
    }

    void OnDestroy()
    {
        Time.timeScale = 0;
        if (gameState != null)
        {
            gameState.SavePlayerData(this);
            gameState.SaveState();
        }
        if (gameOverMenuGameObject != null) gameOverMenuGameObject.SetActive(true);
    }

    void ClampPosition()
    {
        Vector3 position = transform.position;

        var gameXPos = gameAreaRenderer.transform.position.x;
        var gameMinX = gameAreaRenderer.bounds.min.x + gameXPos;
        var gameMaxX = gameAreaRenderer.bounds.max.x + gameXPos;

        var gameZPos = gameAreaRenderer.transform.position.z;
        var gameMinZ = gameAreaRenderer.bounds.min.z;
        var gameMaxZ = gameAreaRenderer.bounds.max.z;

        position.x = Mathf.Clamp(position.x, gameMinX, gameMaxX);
        position.z = Mathf.Clamp(position.z, gameMinZ, gameMaxZ);

        transform.position = position;
    }

    void Move()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(xMove, rb.velocity.y, zMove) * speed;
        ClampPosition();
    }

    void Shoot()
    {
        if (Input.GetButton("Fire1")) primaryCannon.Shoot();
    }

    void PauseUnpauseGame()
    {
        if (Input.GetButtonDown("Cancel") && !cheatsMenuGameObject.activeSelf)
        {
            if (pauseMenuGameObject.activeSelf)
            {
                Time.timeScale = 1;
                pauseMenuGameObject.SetActive(false);
                return;
            }

            Time.timeScale = 0;
            pauseMenuGameObject.SetActive(true);
        }

    }

    void OpenCheatsMenu()
    {
        if (Input.GetKeyDown(KeyCode.O) && !pauseMenuGameObject.activeSelf)
        {
            if (cheatsMenuGameObject.activeSelf)
            {
                Time.timeScale = 1;
                cheatsMenuGameObject.SetActive(false);
                return;
            }

            Time.timeScale = 0;
            cheatsMenuGameObject.SetActive(true);
        }
    }

    void PlayerControls()
    {
        if (Time.timeScale > 0)
        {
            Move();
            Shoot();
            UseAbility();
        }
        PauseUnpauseGame();
        OpenCheatsMenu();
    }

    void Update()
    {
        PlayerControls();
    }

    override public void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Collectable")
        {
            var collectable = other.gameObject.GetComponent<Collectable>();
            if (collectable.collectableType == CollectableType.HEALTH && life < maxLife)
            {
                life += 1;
                OnLifeChange();
            }

            if (collectable.collectableType == CollectableType.SHIELD && shield < maxShield)
            {
                shield += 1;
                OnShieldChange();
            }

            if (collectable.collectableType == CollectableType.WEAPON) StartCoroutine(BuffWeapon());

            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "TrailBox")
        {
            Destroy(other.gameObject);
            trailsMenu.OpenMenu();
        }
    }

    void OnLifeChange()
    {
        onLifeChange.Invoke(life);
    }

    void OnShieldChange()
    {
        onShieldChange.Invoke(shield);
    }

    override public void DetectHit(Collider other)
    {
        base.DetectHit(other);
        OnLifeChange();
        OnShieldChange();
    }

    IEnumerator BuffWeapon()
    {
        weaponIsBuffedCount += 1;
        primaryCannon.damageBuff = weaponBuffDamage;
        yield return new WaitForSeconds(weaponBuffDuration);
        weaponIsBuffedCount -= 1;

        if (weaponIsBuffedCount == 0) primaryCannon.damageBuff = 0;
    }

    public void BoxUpdate(TrailsType boxType)
    {
        if (trailsSum >= 4) return;

        if (boxType == TrailsType.PLASMA)
        {
            plasmaTrail += 1;
            onTrailsChange.Invoke();
        }
        if (boxType == TrailsType.MISSILE)
        {
            missileTrail += 1;
            onTrailsChange.Invoke();
        }
        if (boxType == TrailsType.LASER)
        {
            laserTrail += 1;
            onTrailsChange.Invoke();
        }

        CheckTrails();
        BuildShip();
    }

    public void CheckTrails()
    {
        CheckPlasmaTrail();
        CheckMissileTrail();
        CheckLaserTrail();
    }

    public void CheckPlasmaTrail()
    {
        if (plasmaTrail > 1)
        {
            primaryWeapon = ProjectileType.PLASMA;
        }

        if (plasmaTrail > 0 && !plasmaBuffApplied)
        {
            permaDamageBuff += plasmaBuffDamage;
            plasmaBuffApplied = true;
        }
    }

    public void CheckMissileTrail()
    {
        if (missileTrail > 2)
        {
            // robozin que roda
        }

        if (missileTrail > 1)
        {
            primaryWeapon = ProjectileType.MISSILE;
        }

        if (missileTrail > 0 && !missileBuffApplied)
        {
            maxLife += missileBuffLife;
            missileBuffApplied = true;
        }
    }

    public void CheckLaserTrail()
    {
        if (laserTrail > 2 && !markerIsOn)
        {
            markerIsOn = true;
            markerGameObject.SetActive(true);
            StartCoroutine(MarkerMovimentation());
        }

        if (laserTrail > 1)
        {
            primaryWeapon = ProjectileType.LASER;
        }

        if (laserTrail > 0 && !laserBuffApplied)
        {
            speed += laserTrailBuffSpeed;
            laserBuffApplied = true;
        }
    }

    public void ResetTrails()
    {
        plasmaTrail = 0;
        missileTrail = 0;
        laserTrail = 0;
        plasmaBuffApplied = false;
        missileBuffApplied = false;
        laserBuffApplied = false;
        primaryWeapon = ProjectileType.BULLET;
        permaDamageBuff -= plasmaBuffDamage;
        speed -= laserTrailBuffSpeed;
        maxLife -= missileBuffLife;
        BuildShip();
    }

    void LaserAbility()
    {
        StartCoroutine(InvencibleTick());
        transform.position = markerGameObject.transform.position;
        markerGameObject.SetActive(false);
        StartCoroutine(AbilityCooldown(() => markerGameObject.SetActive(true)));
    }

    void PlasmaAbility()
    {
        Instantiate(plasmaExplosionPrefab, transform.position, Quaternion.identity);
        StartCoroutine(AbilityCooldown());
    }

    void MissileAbility()
    {
        var position = transform.position;
        position.x += 2f;

        missileCompanionGameObject = Instantiate(missileCompanionPrefab, position, Quaternion.identity);
        missileCompanionGameObject.transform.parent = transform;
        StartCoroutine(DestroyMissileCompanion());
    }

    void UseAbility()
    {
        if (Input.GetButtonDown("Fire2") && !abilityIsOnCooldown)
        {
            if (laserTrail > 2) LaserAbility();
            else if (plasmaTrail > 2) PlasmaAbility();
            else if (missileTrail > 2) MissileAbility();
        }
    }

    IEnumerator AbilityCooldown(Action reset = null)
    {
        abilityIsOnCooldown = true;
        yield return new WaitForSeconds(abilityCooldown);
        abilityIsOnCooldown = false;
        if (reset != null) reset();
    }

    IEnumerator DestroyMissileCompanion()
    {
        abilityIsOnCooldown = true;
        yield return new WaitForSeconds(5f);
        Destroy(missileCompanionGameObject);
        StartCoroutine(AbilityCooldown());
    }

    IEnumerator MarkerMovimentation()
    {
        previousPositions.Add(transform.position);
        markerGameObject.transform.position = previousPositions[0];
        yield return new WaitForSeconds(2f);

        while (true)
        {
            previousPositions.Add(transform.position);
            yield return new WaitForSeconds(0.1f);
            if (previousPositions.Count > 0) markerGameObject.transform.position = previousPositions[0];
            if (previousPositions.Count > 10) previousPositions.RemoveAt(0);
        }
    }

    IEnumerator InvencibleTick()
    {
        invencible = true;
        yield return new WaitForSeconds(.5f);
        invencible = false;
    }

    public void BuildShip()
    {
        SetPrimaryWepon();
    }

    public void SetPrimaryWepon()
    {
        primaryWeaponCannonPrefab = GetCannonPrefab(primaryWeapon);
        GetPrimaryCannonLocation();
        var primaryCannonObject = Instantiate(primaryWeaponCannonPrefab, primaryCannonLocation.position, transform.rotation);
        primaryCannonObject.transform.Rotate(90, 0, 0);
        primaryCannonObject.transform.parent = gameObject.transform;
        primaryCannon = primaryCannonObject.GetComponent<Cannon>();
        primaryCannon.DefineTeam(team);
        primaryCannon.permaDamageBuff = permaDamageBuff;
    }

    GameObject GetCannonPrefab(ProjectileType type)
    {
        switch (type)
        {
            case ProjectileType.BULLET:
                return bulletCannonPrefab;
            case ProjectileType.LASER:
                return laserCannonPrefab;
            case ProjectileType.PLASMA:
                return plasmaCannonPrefab;
            case ProjectileType.MISSILE:
                return missileCannonPrefab;
            default:
                return bulletCannonPrefab;
        }
    }

    public void GetPrimaryCannonLocation()
    {
        primaryCannonLocation = transform.Find("PrimaryCannonLocation");
    }

}
