using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    public int team = 0;
    public int projectileDirection = 1;
    public int life = 3;

    public Dictionary<ProjectileType, GameObject> weapons = new Dictionary<ProjectileType, GameObject>();

    void DetectHit(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            var projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile.team != team)
            {
                projectile.Destroy();
                life -= projectile.damage;

                if (life >= 0)
                {
                    OnLifeZero();
                }
            }
        }
    }

    void Shoot(ProjectileType type = ProjectileType.BULLET)
    {
        var projectile = weapons[type];

        if (projectile == null)
        {
            return;
        }

        projectile.GetComponent<Projectile>().Define(team, projectileDirection);
        Instantiate(projectile, transform.position, Quaternion.identity);
    }

    void OnLifeZero()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        DetectHit(collision);
    }

}
