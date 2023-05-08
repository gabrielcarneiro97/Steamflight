using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Ship
{
    public GameObject pauseMenuGameObject;
    public UnityEvent<int> onLifeChange;
    public UnityEvent<int> onShieldChange;

    // Powerups
    int weaponIsBuffedCount = 0;
    float weaponBuffDuration = 5f;
    int weaponBuffDamage = 2;

    // Updates
    int plasmaTrail = 0;
    int plasmaBuffDamage = 1;
    bool plasmaBuffApplied = false;
    int missileTrail = 0;
    int missileBuffLife = 1;
    bool missileBuffApplied = false;
    int laserTrail = 0;
    float laserTrailBuffSpeed = 3f;
    bool laserBuffApplied = false;

    void Start()
    {
        pauseMenuGameObject.SetActive(false);
        rb = gameObject.GetComponent<Rigidbody>();
        team = Team.PLAYER;
        life = 3;
        maxLife = 5;
        BuildShip();
    }

    void Move()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(xMove, rb.velocity.y, zMove) * speed;
    }

    void Shoot()
    {
        if (Input.GetButton("Fire1")) primaryCannon.Shoot();
    }

    void PauseUnpauseGame()
    {
        if (Input.GetButtonDown("Cancel"))
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

    void PlayerControls()
    {
        if (Time.timeScale > 0)
        {
            Move();
            Shoot();
        }
        PauseUnpauseGame();
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

    public void BoxUpdate(string boxType)
    {
        if (boxType == "PlasmaCannon") plasmaTrail += 1;
        if (boxType == "MissileCannon") missileTrail += 1;
        if (boxType == "LaserCannon") laserTrail += 1;

        CheckPlasmaTrail();
        CheckMissileTrail();
        CheckLaserTrail();
        BuildShip();
    }

    public void CheckPlasmaTrail()
    {
        if (plasmaTrail > 2)
        {
            // explosão envolta do jogador que cria explosoes quando colide com inimigos
        }

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
        if (laserTrail > 2)
        {
            // tp 2 seg atrás
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

}
