using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    public Team team = Team.NEUTRAL;
    public int life = 3;

    void DetectHit(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            var projectile = other.gameObject.GetComponent<Projectile>();
            if (projectile.team != team && projectile.team != Team.NEUTRAL)
            {
                life -= projectile.damage;
                Destroy(other.gameObject);

                if (life <= 0)
                {
                    OnLifeZero();
                }
            }
        }
    }

    void OnLifeZero()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        DetectHit(other);
    }

}
