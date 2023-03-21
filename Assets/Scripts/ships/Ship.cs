using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    public float speed;
    public Team team = Team.NEUTRAL;
    public int life = 3;
    public ProjectileType primaryWeapon = ProjectileType.BULLET;
    GameObject primaryWeaponCannonPrefab;
    Transform primaryCannonLocation;
    [HideInInspector]
    public Cannon primaryCannon;
    [HideInInspector]
    public Rigidbody rb;
    Vector3 lastPostion;

    public Rotator rotator;

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
    }

    GameObject GetCannonPrefab(ProjectileType type)
    {
        switch (type)
        {
            case ProjectileType.BULLET:
                return Resources.Load<GameObject>("Prefabs/BulletCannon");
            default:
                return null;
        }
    }

    public void GetPrimaryCannonLocation()
    {
        primaryCannonLocation = transform.Find("PrimaryCannonLocation");
    }
}
