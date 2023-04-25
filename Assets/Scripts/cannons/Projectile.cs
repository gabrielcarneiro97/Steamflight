using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public Team team = Team.NEUTRAL;
    public int damage = 0;
    public float speed = 0f;

    public ProjectileType type = ProjectileType.NONE;

    virtual public void Start()
    {
        var rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    public void DefineTeam(Team team)
    {
        this.team = team;
    }

    public void BuffDamage(int buff)
    {
        damage += buff;
    }

    void Travel()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }

    // Update is called once per frame
    virtual public void Update()
    {
        Travel();
    }
}
