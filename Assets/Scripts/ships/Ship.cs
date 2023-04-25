using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    public bool isActive = false;
    public float speed;
    public Team team = Team.NEUTRAL;
    public int life = 3;
    public int shield = 0;
    public int maxLife = 3;
    public int maxShield = 2;
    public int permaDamageBuff = 0;
    public ProjectileType primaryWeapon = ProjectileType.BULLET;
    GameObject primaryWeaponCannonPrefab;
    Transform primaryCannonLocation;
    [HideInInspector]
    public Cannon primaryCannon;
    [HideInInspector]
    public Rigidbody rb;
    Vector3 lastPostion;

    public GameObject bulletCannonPrefab;
    public GameObject laserCannonPrefab;

    virtual public void DetectHit(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            var projectile = other.gameObject.GetComponent<Projectile>();
            if (projectile.team != team && projectile.team != Team.NEUTRAL)
            {
                var damage = projectile.damage;

                if (shield > 0)
                {
                    shield -= damage;
                    if (shield < 0)
                    {
                        damage = -shield;
                        shield = 0;
                    }
                    else damage = 0;
                }

                if (isActive) life -= damage;
                if (life <= 0) OnLifeZero();

                Destroy(other.gameObject);
            }
        }
    }

    void OnLifeZero()
    {
        Destroy(gameObject);
    }

    virtual public void OnTriggerEnter(Collider other)
    {
        DetectHit(other);
    }

    public void BuildShip()
    {
        SetPrimaryWepon();
    }

    public void SetPrimaryWepon()
    {
        primaryWeaponCannonPrefab = GetCannonPrefab(primaryWeapon);
        GetPrimaryCannonLocation();
        var primaryCannonObject = Instantiate(primaryWeaponCannonPrefab, primaryCannonLocation.position, transform.rotation);
        primaryCannonObject.transform.Rotate(90, 0, 0);
        primaryCannonObject.transform.parent = gameObject.transform;
        primaryCannon = primaryCannonObject.GetComponent<Cannon>();
        primaryCannon.DefineTeam(team);
        primaryCannon.permaDamageBuff = permaDamageBuff;
    }

    GameObject GetCannonPrefab(ProjectileType type)
    {
        switch (type)
        {
            case ProjectileType.BULLET:
                return bulletCannonPrefab;
            case ProjectileType.LASER:
                return laserCannonPrefab;
            default:
                return bulletCannonPrefab;
        }
    }

    public void GetPrimaryCannonLocation()
    {
        primaryCannonLocation = transform.Find("PrimaryCannonLocation");
    }
}
