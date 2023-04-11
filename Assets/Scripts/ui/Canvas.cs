using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Canvas : MonoBehaviour
{
    public GameObject scoreTextGameObject;
    TMP_Text scoreText;
    int score = 0;

    public GameObject lifeTextGameObject;
    TMP_Text lifeText;
    EnemyManager enemyManager;
    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        enemyManager = FindObjectOfType<EnemyManager>();

        enemyManager.onEnemyDeath.AddListener(ChangeScore);
        scoreText = scoreTextGameObject.GetComponent<TMP_Text>();

        player.onLifeChange.AddListener(ChangeLife);
        lifeText = lifeTextGameObject.GetComponent<TMP_Text>();

        lifeText.text = "Life: " + player.life;
    }

    void ChangeScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    void ChangeLife(int life)
    {
        lifeText.text = "Life: " + life;
    }
}
