using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    public Team team = Team.NEUTRAL;
    public int life = 3;

    public Dictionary<ProjectileType, GameObject> weapons = new Dictionary<ProjectileType, GameObject>();

    public Dictionary<ProjectileType, bool> weaponsIsAvaible = new Dictionary<ProjectileType, bool>();

    public void Start()
    {
        weapons.Add(ProjectileType.BULLET, Resources.Load<GameObject>("Prefabs/Bullet"));
        weaponsIsAvaible.Add(ProjectileType.BULLET, true);
    }


    void DetectHit(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            var projectile = other.gameObject.GetComponent<Projectile>();
            if (projectile.team != team)
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

    public void Shoot(ProjectileType type = ProjectileType.BULLET)
    {
        var projectile = weapons[type];

        if (projectile == null)
        {
            return;
        }

        if (!weaponsIsAvaible[type])
        {
            return;
        }

        var cooldown = projectile.GetComponent<Projectile>().cooldown;
        projectile.GetComponent<Projectile>().Define(team);
        Instantiate(projectile, transform.position, Quaternion.identity);
        StartCoroutine(CooldownWeapon(type, cooldown));
    }

    IEnumerator CooldownWeapon(ProjectileType type, float cooldown)
    {
        weaponsIsAvaible[type] = false;
        yield return new WaitForSeconds(cooldown);
        weaponsIsAvaible[type] = true;
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
