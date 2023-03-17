using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Ship
{
    // Start is called before the first frame update
    void Start()
    {
        team = Team.ENEMY;
        life = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
