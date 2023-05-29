using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy : Ship
{
    public EnemyManager enemyManager;
    bool despawned = false;
    public bool walker = false;
    public bool walkerLoop = false;
    public bool wakerRepeat = false;
    public int points = 10;
    SplineWalker splineWalker;

    public GameObject[] cannonsGameObjects;
    List<Cannon> cannons = new List<Cannon>();

    public void Start()
    {
        team = Team.ENEMY;
        enemyManager = FindObjectOfType<EnemyManager>();
        enemyManager.AddEnemy(gameObject);

        if (walker)
        {
            var spline = GetComponent<SplineContainer>().Spline;
            splineWalker = new SplineWalker(spline, transform, speed, walkerLoop, wakerRepeat);
        }

        foreach (var cannonGameObject in cannonsGameObjects)
        {
            var cannon = cannonGameObject.GetComponent<Cannon>();
            cannon.DefineTeam(team);
            cannons.Add(cannon);
        }
    }

    void Shoot()
    {
        foreach (var cannon in cannons)
            cannon.Shoot();
    }

    public void Update()
    {
        if (isActive) Shoot();
        if (walker && isActive) splineWalker.MoveOnSpline();
    }

    void OnDestroy()
    {
        if (enemyManager != null) enemyManager.RemoveEnemy(gameObject, despawned ? 0 : points);
    }

    public void Despawn()
    {
        despawned = true;
        Destroy(gameObject);
    }
}
