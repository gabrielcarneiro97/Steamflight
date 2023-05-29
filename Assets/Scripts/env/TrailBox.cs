using UnityEngine;

public class TrailBox : MonoBehaviour
{
    public int boxNumber;
    public Player player;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        player.onStateLoad.AddListener(CheckBoxStatus);
    }

    void CheckBoxStatus()
    {
        if (player.trailsSum >= boxNumber) gameObject.SetActive(false);
    }

}
