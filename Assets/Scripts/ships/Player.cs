using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Ship
{
    public UnityEvent<int> onLifeChange;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        team = Team.PLAYER;
        life = 3;
        maxLife = 5;
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

    void Update()
    {
        PlayerControls();
    }

    override public void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Collectable")
        {
            var collectable = other.gameObject.GetComponent<Collectable>();
            if (collectable.collectableType == CollectableType.HEALTH && life < maxLife)
            {
                life += 1;
                OnLifeChange();
            }
            Destroy(other.gameObject);
        }
    }

    void OnLifeChange()
    {
        Debug.Log("Life: " + life);
        onLifeChange.Invoke(life);
    }

    override public void DetectHit(Collider other)
    {
        Debug.Log("Detect Hit");
        base.DetectHit(other);
        OnLifeChange();
    }

}
