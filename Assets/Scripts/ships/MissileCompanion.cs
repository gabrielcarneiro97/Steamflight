using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCompanion : Ship
{
    public GameObject cannonGameObject;
    Cannon cannon;

    void Start()
    {
        invencible = true;
        cannon = cannonGameObject.GetComponent<Cannon>();

    }

    // Update is called once per frame
    void Update()
    {
        cannon.Shoot();
    }
}
