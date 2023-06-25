using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject MenuCanvas;
    public GameObject ControlsCanvas;

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

    public void SwitchMenuControl()
    {
        MenuCanvas.SetActive(!MenuCanvas.activeSelf);
        ControlsCanvas.SetActive(!ControlsCanvas.activeSelf);
    }
}
