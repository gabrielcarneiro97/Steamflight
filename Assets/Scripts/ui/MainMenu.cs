using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameState gameState;

    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        if (tag == "Continue")
            gameState.onStateLoaded.AddListener(ActiveContinueBtn);


    }

    void ActiveContinueBtn()
    {
        if (!gameState.hasState) return;

        var btn = GetComponent<UnityEngine.UI.Button>();
        btn.interactable = true;
    }

    public void StartGame()
    {
        gameState.ClearState();
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(gameState.state.level);
    }
}
