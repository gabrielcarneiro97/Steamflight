using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTower : Enemy
{
    GameObject target;

    new void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Rotate()
    {
        var rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = rotation;
        // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * .2f);
    }

    new void Update()
    {
        base.Update();
        if (isActive) Rotate();
    }
}
