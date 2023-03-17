using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public Team team = Team.NEUTRAL;
    public int damage = 0;
    public float speed = 0f;

    public ProjectileType type = ProjectileType.NONE;

    // cooldown in seconds
    public float cooldown = .3f;

    public void Start()
    {
        var rb = gameObject.GetComponent<Rigidbody>();
        gameObject.transform.Rotate(90, 0, 0);
        rb.freezeRotation = true;
    }
    public void Define(Team team)
    {
        this.team = team;
    }

    void Travel()
    {
        // Vector3 move = new Vector3(0, 1, 0);
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
        // transform.forward = move * speed * Time.deltaTime;
    }

    // Update is called once per frame
    public void Update()
    {
        Travel();
    }
}
