using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public void OnContinue()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
