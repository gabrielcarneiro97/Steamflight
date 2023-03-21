using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy : Ship
{
    public bool canShoot = false;
    public bool walker = false;
    public bool walkerLoop = false;
    public bool wakerRepeat = false;

    SplineWalker splineWalker;
    // Start is called before the first frame update
    void Start()
    {
        team = Team.ENEMY;
        BuildShip();

        if (walker)
        {
            var spline = GetComponent<SplineContainer>().Spline;
            splineWalker = new SplineWalker(spline, transform, speed, walkerLoop, wakerRepeat);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot) primaryCannon.Shoot();
        if (walker) splineWalker.MoveOnSpline();
        Rotate();
    }
}
