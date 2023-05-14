using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawnTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            var enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.Despawn();
        }
    }
}
