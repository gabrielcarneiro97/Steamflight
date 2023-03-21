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
    Rigidbody rb;

    Vector3 lastPostion;

    public bool isPlayer = false;

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

    void Rotation(float rotationSpeed, int maxRotation, float velX)
    {
        var facingForward = transform.rotation.eulerAngles.y == 0;
        var rotZ = transform.rotation.eulerAngles.z;

        var maxLeftRotation = maxRotation;
        var maxRightRotation = -maxRotation;

        var convertRotZ = rotZ > 180 ? rotZ - 360 : rotZ;

        if (velX != 0)
        {
            var directionX = facingForward ? (velX > 0 ? 1 : -1) : velX > 0 ? -1 : 1;
            // directionX + => right
            // directionX - => left

            if ((convertRotZ > maxRightRotation && directionX == 1) || (convertRotZ < maxLeftRotation && directionX == -1) || convertRotZ == 0)
            {
                var angle = Mathf.Clamp(convertRotZ - directionX * rotationSpeed * Time.deltaTime, maxRightRotation, maxLeftRotation);
                transform.rotation = (Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle));
            }
        }
        else
        {
            if (convertRotZ > 3 && (int)convertRotZ <= maxLeftRotation)
            {
                transform.rotation = (Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, convertRotZ - rotationSpeed * Time.deltaTime));
            }
            else if (convertRotZ < -3 && (int)convertRotZ >= maxRightRotation)
            {
                transform.rotation = (Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, convertRotZ + rotationSpeed * Time.deltaTime));
            }
        }
    }

    void RbRotation(float rotationSpeed, int maxRotation)
    {
        if (rb == null) rb = gameObject.GetComponent<Rigidbody>();
        var velX = rb.velocity.x;

        Rotation(rotationSpeed, maxRotation, velX);
    }

    void NonRbRotation(float rotationSpeed, int maxRotation)
    {
        var velX = transform.position.x - lastPostion.x;
        Rotation(rotationSpeed, maxRotation, velX);
        lastPostion = transform.position;
    }

    public void Rotate()
    {
        // var maxRightRotation = -45;
        // var maxLeftRotation = 45;
        var maxRotation = 45;
        var rotationSpeed = 100f;

        if (isPlayer) RbRotation(rotationSpeed, maxRotation);
        else NonRbRotation(rotationSpeed, maxRotation);
    }
}
