using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCleaner : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("clean!");
        if (collider.gameObject.tag == "Projectile")
        {
            Destroy(collider.gameObject);
        }
    }
}
