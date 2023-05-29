using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void HandleBackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void HandleTryAgainLevel1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void HandleTryAgainLevel2()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
}

