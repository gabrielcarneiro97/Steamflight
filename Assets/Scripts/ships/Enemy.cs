using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy : Ship
{
    public bool walker = false;
    public bool walkerLoop = false;
    public bool wakerRepeat = false;
    public int points = 10;
    SplineWalker splineWalker;

    public EnemyManager enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        team = Team.ENEMY;
        BuildShip();
        enemyManager = FindObjectOfType<EnemyManager>();
        enemyManager.AddEnemy(gameObject);

        if (walker)
        {
            var spline = GetComponent<SplineContainer>().Spline;
            splineWalker = new SplineWalker(spline, transform, speed, walkerLoop, wakerRepeat);
        }
    }

    void Update()
    {
        if (isActive) primaryCannon.Shoot();
        if (walker && isActive) splineWalker.MoveOnSpline();
    }

    void OnDestroy()
    {
        enemyManager.RemoveEnemy(gameObject, points);
    }
}
