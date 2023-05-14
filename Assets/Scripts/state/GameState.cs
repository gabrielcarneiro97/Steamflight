
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

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
    public State state = new State();
    public String jsonState
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

    public UnityEvent onStateLoaded = new UnityEvent();


    void Start()
    {
        jsonPath = Application.persistentDataPath + "/state.json";
        OpenFile();
        var sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex > 0)
        {
            state.level = sceneIndex;
        }
        state.score = 0;

        Debug.Log(jsonState);
        onStateLoaded.Invoke();
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

    void OpenFile()
    {
        if (!File.Exists(jsonPath))
        {
            File.WriteAllText(jsonPath, jsonState);
            return;
        }

        var fileStream = File.ReadAllText(jsonPath);
        state = JsonUtility.FromJson<State>(fileStream);
    }

    public void SaveState()
    {
        File.WriteAllText(jsonPath, jsonState);
    }

    public void ClearState()
    {
        state = new State();
        File.WriteAllText(jsonPath, jsonState);
    }
}
