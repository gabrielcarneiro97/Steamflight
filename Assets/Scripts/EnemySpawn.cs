using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-1f * Time.deltaTime, 0, 0);
    }

    void OnDestroy()
    {
        // spawn enemy
        Debug.Log("Enemy spawned!");
    }
}
