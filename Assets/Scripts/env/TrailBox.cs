using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBox : MonoBehaviour
{
    public int boxNumber;
    public Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.onStateLoaded.AddListener(CheckBoxStatus);
    }

    void CheckBoxStatus()
    {
        if (player.trailsSum >= boxNumber)
        {
            gameObject.SetActive(false);
        }
    }

}
