using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public void HandleContinue()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void HandleBackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
