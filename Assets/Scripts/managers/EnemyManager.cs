using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public UnityEvent<int> onEnemyDeath;
    public List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy, int points = 10)
    {
        enemies.Remove(enemy);
        onEnemyDeath.Invoke(points);
    }
}
