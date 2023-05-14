using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCompanion : MonoBehaviour
{
    public GameObject cannonGameObject;
    Cannon cannon;

    void Start()
    {
        cannon = cannonGameObject.GetComponent<Cannon>();

    }

    // Update is called once per frame
    void Update()
    {
        cannon.Shoot();
    }
}
