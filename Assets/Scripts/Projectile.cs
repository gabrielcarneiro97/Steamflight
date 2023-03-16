using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int team = 0;
    public int damage = 0;
    public int direction = 0;
    public float speed = 0f;

    public void Define(int team, int direction)
    {
        this.team = team;
        this.direction = direction;
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }

    void Travel()
    {
        transform.Translate(0, 0, speed * Time.deltaTime * direction);
    }

    // Update is called once per frame
    void Update()
    {
        Travel();
    }
}
