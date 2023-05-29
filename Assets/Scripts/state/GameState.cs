
using System;
using UnityEngine;

[Serializable]
public class PlayerState
{
    public int missileTrail = 0;
    public int plasmaTrail = 0;
    public int laserTrail = 0;

}

[Serializable]
public class State
{
    public int score
    {
        get
        {
            return scoreLevel1 + scoreLevel2;
        }
        set
        {
            if (level == 1)
                scoreLevel1 = value;
            else if (level == 2)
                scoreLevel2 = value;
        }
    }
    public int scoreLevel1;
    public int scoreLevel2;
    public int level;
    public PlayerState playerState = new PlayerState();
}

public class GameState : MonoBehaviour
{
    String jsonPath;

    public static GameState gameState;
    public State state = new State();

    public string json
    {
        get
        {
            return JsonUtility.ToJson(state);
        }
    }
    public bool hasState
    {
        get
        {
            return state.level > 0;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (gameState == null) gameState = this;
        else Destroy(this.gameObject);
    }

    public void SetLevel(int level)
    {
        state.level = level;
    }

    public void SavePlayerData(Player player)
    {
        state.playerState.missileTrail = player.missileTrail;
        state.playerState.plasmaTrail = player.plasmaTrail;
        state.playerState.laserTrail = player.laserTrail;
    }

    public void LoadPlayerData(Player player)
    {
        player.missileTrail = state.playerState.missileTrail;
        player.plasmaTrail = state.playerState.plasmaTrail;
        player.laserTrail = state.playerState.laserTrail;
    }

    public void ClearState()
    {
        state = new State();
    }

}
