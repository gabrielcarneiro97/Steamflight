using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : Projectile
{
    public GameObject explosionPrefab;
    PlasmaExplosion explosion;
    override public void Start()
    {
        base.Start();
        explosion = explosionPrefab.GetComponent<PlasmaExplosion>();
        explosion.DefineTeam(team);
    }

    override public void Update()
    {
        base.Update();
    }

    void OnDestroy()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
