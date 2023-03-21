using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        type = ProjectileType.BULLET;
        damage = 1;
        speed = 10f;
        cooldown = 1f;
    }


    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
