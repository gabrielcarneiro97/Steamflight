using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision with " + collider.gameObject.name);
        if (collider.gameObject.name == "EnemySpawn")
        {
            Destroy(collider.gameObject);
        }
    }
}
