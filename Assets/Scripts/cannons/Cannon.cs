using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectilePrefab;
    bool isAvaible = true;
    public float cooldown = 1f;
    public ProjectileType type = ProjectileType.BULLET;
    public Team team = Team.NEUTRAL;
    Transform shootPoint;

    public int damageBuff = 0;

    public int permaDamageBuff = 0;

    void Start()
    {
        shootPoint = transform.Find("ShootPoint");
    }

    public void DefineTeam(Team team)
    {
        this.team = team;
    }

    public void Shoot()
    {
        if (projectilePrefab == null)
        {
            return;
        }

        if (!isAvaible)
        {
            return;
        }

        var projectile = projectilePrefab.GetComponent<Projectile>();
        projectile.DefineTeam(team);
        projectile.BuffDamage(damageBuff);
        projectile.BuffDamage(permaDamageBuff);
        Instantiate(projectilePrefab, shootPoint.position, transform.rotation);
        StartCoroutine(CooldownWeapon());
    }


    IEnumerator CooldownWeapon()
    {
        isAvaible = false;
        yield return new WaitForSeconds(cooldown);
        isAvaible = true;
    }
}
