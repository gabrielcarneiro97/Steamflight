using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class FinalBoss : Ship
{
    public GameObject bossAreaGameObject;
    Renderer bossAreaRenderer;
    Renderer bossRenderer;
    float objectWidth;
    float objectHeight;
    GameObject target;
    public List<GameObject> cannonsGameObjects = new List<GameObject>();
    List<Cannon> cannons = new List<Cannon>();

    public GameObject bulletRainGameObject;
    List<Cannon> bulletRainCannons = new List<Cannon>();
    bool bulletRainIsAvaible = false;
    public int bulletRainCooldown = 15;

    public GameObject laserSpinnerGameObject;
    bool laserSpinnerIsAvaible = false;
    public int laserSpinnerCooldown = 20;
    Cannon laserSpinnerCannon;

    public GameObject missileXGameObject;
    List<Cannon> missileXCannons = new List<Cannon>();
    bool missileXIsAvaible = false;
    public int missileXCooldown = 30;

    public int points = 100;

    int walkSide = 1;

    public EnemyManager enemyManager;
    void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        enemyManager.AddEnemy(gameObject);

        target = GameObject.FindGameObjectWithTag("Player");
        bossAreaRenderer = bossAreaGameObject.GetComponent<Renderer>();
        bossRenderer = GetComponent<Renderer>();
        objectWidth = bossRenderer.bounds.size.x / 2;
        objectHeight = bossRenderer.bounds.size.z / 2;

        foreach (var cannonGameObject in cannonsGameObjects)
        {
            var cannon = cannonGameObject.GetComponent<Cannon>();
            cannon.DefineTeam(team);
            cannons.Add(cannon);
        }

        foreach (Transform cannonTransform in bulletRainGameObject.transform)
        {
            var cannonGameObject = cannonTransform.gameObject;
            var cannon = cannonGameObject.GetComponent<Cannon>();
            cannon.DefineTeam(team);
            bulletRainCannons.Add(cannon);
        }

        laserSpinnerCannon = laserSpinnerGameObject.GetComponentInChildren<Cannon>();

        foreach (Transform cannonTransform in missileXGameObject.transform)
        {
            var cannonGameObject = cannonTransform.gameObject;
            var cannon = cannonGameObject.GetComponent<Cannon>();
            cannon.DefineTeam(team);
            missileXCannons.Add(cannon);
        }


        StartCoroutine(BulletRainCooldown());
        StartCoroutine(LaserSpinnerCooldown());
        StartCoroutine(MissileXCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            Skills();
            Shoot();
            Rotate();
            Walk();
            ClampPosition();
        }
    }

    void OnDestroy()
    {
        if (enemyManager != null) enemyManager.RemoveEnemy(gameObject, points);
    }

    void Walk()
    {
        var position = transform.position;
        position.x += walkSide * speed * Time.deltaTime;
        transform.position = position;

        if (position.x > 10 || position.x < -10)
            walkSide *= -1;
    }

    void Rotate()
    {
        if (target == null) return;

        var rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = rotation;
    }

    void Shoot()
    {
        foreach (var cannon in cannons)
            cannon.Shoot();
    }

    void ClampPosition()
    {
        Vector3 position = transform.position;


        var gameZPos = bossAreaRenderer.transform.position.z;
        var gameMinZ = bossAreaRenderer.bounds.min.z + objectHeight;
        var gameMaxZ = bossAreaRenderer.bounds.max.z - objectHeight;

        position.z = Mathf.Clamp(position.z, gameMinZ, gameMaxZ);

        transform.position = position;
    }

    void Skills()
    {
        if (bulletRainIsAvaible) StartCoroutine(BulletRain());
        if (laserSpinnerIsAvaible) StartCoroutine(LaserSpinner());
        if (missileXIsAvaible) StartCoroutine(MissileX());
    }

    IEnumerator BulletRain()
    {
        foreach (var cannon in bulletRainCannons)
        {
            yield return new WaitForSeconds(0.3f);
            cannon.Shoot();
        }
        StartCoroutine(BulletRainCooldown());
    }

    IEnumerator BulletRainCooldown()
    {
        bulletRainIsAvaible = false;
        yield return new WaitForSeconds(bulletRainCooldown);
        bulletRainIsAvaible = true;
    }

    IEnumerator LaserSpinner()
    {
        for (int i = 0; i < 20; i++)
        {
            laserSpinnerCannon.Shoot();
            yield return new WaitForSeconds(0.2f);
        }
        StartCoroutine(LaserSpinnerCooldown());
    }

    IEnumerator LaserSpinnerCooldown()
    {
        laserSpinnerIsAvaible = false;
        yield return new WaitForSeconds(laserSpinnerCooldown);
        laserSpinnerIsAvaible = true;
    }

    IEnumerator MissileX()
    {
        for (int i = 0; i < 5; i++)
        {
            foreach (var cannon in missileXCannons)
                cannon.Shoot();

            yield return new WaitForSeconds(2f);
        }

        StartCoroutine(MissileXCooldown());
    }

    IEnumerator MissileXCooldown()
    {
        missileXIsAvaible = false;
        yield return new WaitForSeconds(missileXCooldown);
        missileXIsAvaible = true;
    }
}
