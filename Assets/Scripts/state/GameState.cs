
using System;
using System.IO;
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
    public int points;
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

    void Start()
    {
        jsonPath = Application.persistentDataPath + "/state.json";
        OpenFile();
    }

    void OnDestroy()
    {
        SaveState();
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

    void SaveState()
    {
        File.WriteAllText(jsonPath, jsonState);
    }

    void ClearState()
    {
        state = new State();
        File.WriteAllText(jsonPath, jsonState);
    }
}
