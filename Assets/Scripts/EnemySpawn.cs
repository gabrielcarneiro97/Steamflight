using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    void OnDestroy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
