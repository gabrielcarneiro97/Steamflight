using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Ship
{
    Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        team = Team.PLAYER;
        life = 3;
        isPlayer = true;
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
        Rotate();
    }

}
