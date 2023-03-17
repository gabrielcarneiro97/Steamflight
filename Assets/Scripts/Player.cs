using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Ship
{
    public float speed = 10f;
    Rigidbody rb;
    ProjectileType primaryWeapon = ProjectileType.BULLET;
    GameObject primaryWeaponCannonPrefab;
    Transform primaryCannonLocation;
    Cannon primaryCannon;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        team = Team.PLAYER;
        life = 3;
        BuildShip();
    }

    void Move()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(xMove, rb.velocity.y, zMove) * speed;
    }

    void Shoot()
    {
        if (Input.GetButton("Fire1"))
        {
            primaryCannon.Shoot();
        }
        // base.Shoot(ProjectileType.BULLET);
    }

    void PlayerControls()
    {
        Move();
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControls();
    }

    void CannonsLoader()
    {

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

    void GetPrimaryCannonLocation()
    {
        primaryCannonLocation = transform.Find("PrimaryCannonLocation");
    }

    void BuildShip()
    {
        SetPrimaryWepon();
    }

    void SetPrimaryWepon()
    {
        primaryWeaponCannonPrefab = GetCannonPrefab(primaryWeapon);
        GetPrimaryCannonLocation();
        var primaryCannonObject = Instantiate(primaryWeaponCannonPrefab, primaryCannonLocation.position, Quaternion.identity);
        primaryCannonObject.transform.Rotate(90, 0, 0);
        primaryCannonObject.transform.parent = gameObject.transform;
        primaryCannon = primaryCannonObject.GetComponent<Cannon>();
        primaryCannon.DefineTeam(team);
    }
}
