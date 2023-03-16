using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public float speed = 2f;
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
