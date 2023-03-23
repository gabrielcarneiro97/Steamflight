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
    public GameObject splineObject;
    // Start is called before the first frame update
    void Start()
    {
        team = Team.ENEMY;
        BuildShip();
        rotator = new Rotator(45, 100f, transform);

        if (walker)
        {
            var spline = splineObject.GetComponent<SplineContainer>().Spline;
            splineWalker = new SplineWalker(spline, transform, speed, walkerLoop, wakerRepeat);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot) primaryCannon.Shoot();
        if (walker) splineWalker.MoveOnSpline();
        rotator.Rotate();
        splineObject.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
