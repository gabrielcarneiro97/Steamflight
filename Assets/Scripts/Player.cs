using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Ship
{
    public float speed = 10f; //Controls velocity multiplier

    Rigidbody rb;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        rb = gameObject.GetComponent<Rigidbody>();
        team = Team.PLAYER;
        life = 3;
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(xMove, rb.velocity.y, zMove) * speed;


        Shoot();
    }
}
