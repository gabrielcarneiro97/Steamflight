using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    GameState gameState;

    void Start()
    {
        gameState = GameState.gameState;
        ActiveContinueBtn();
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
