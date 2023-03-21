using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    GameObject projectilePrefab;
    bool isAvaible = true;
    public float cooldown = 1f;
    public ProjectileType type = ProjectileType.BULLET;
    public Team team = Team.NEUTRAL;
    Transform shootPoint;
    // Start is called before the first frame update
    void Start()
    {
        projectilePrefab = GetProjectilePrefab();
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

        projectilePrefab.GetComponent<Projectile>().DefineTeam(team);
        Instantiate(projectilePrefab, shootPoint.position, transform.rotation);
        StartCoroutine(CooldownWeapon());
    }


    IEnumerator CooldownWeapon()
    {
        isAvaible = false;
        yield return new WaitForSeconds(cooldown);
        isAvaible = true;
    }

    GameObject GetProjectilePrefab()
    {
        switch (type)
        {
            case ProjectileType.BULLET:
                return Resources.Load<GameObject>("Prefabs/Bullet");
            default:
                return null;
        }
    }
}
