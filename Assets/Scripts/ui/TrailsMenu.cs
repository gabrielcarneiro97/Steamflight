using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailsMenu : MonoBehaviour
{
    public GameObject playerGameObject;
    Player player;

    public GameObject missile1GameObject;
    Button missile1Btn;
    public GameObject missile2GameObject;
    Button missile2Btn;
    public GameObject missile3GameObject;
    Button missile3Btn;

    public GameObject laser1GameObject;
    Button laser1Btn;
    public GameObject laser2GameObject;
    Button laser2Btn;
    public GameObject laser3GameObject;
    Button laser3Btn;

    public GameObject plasma1GameObject;
    Button plasma1Btn;
    public GameObject plasma2GameObject;
    Button plasma2Btn;
    public GameObject plasma3GameObject;
    Button plasma3Btn;



    void Start()
    {
        player = playerGameObject.GetComponent<Player>();

        missile1Btn = missile1GameObject.GetComponent<Button>();
        missile1Btn.onClick.AddListener(HandleMissileTrail);
        missile2Btn = missile2GameObject.GetComponent<Button>();
        missile2Btn.onClick.AddListener(HandleMissileTrail);
        missile3Btn = missile3GameObject.GetComponent<Button>();
        missile3Btn.onClick.AddListener(HandleMissileTrail);

        laser1Btn = laser1GameObject.GetComponent<Button>();
        laser1Btn.onClick.AddListener(HandleLaserTrail);
        laser2Btn = laser2GameObject.GetComponent<Button>();
        laser2Btn.onClick.AddListener(HandleLaserTrail);
        laser3Btn = laser3GameObject.GetComponent<Button>();
        laser3Btn.onClick.AddListener(HandleLaserTrail);

        plasma1Btn = plasma1GameObject.GetComponent<Button>();
        plasma1Btn.onClick.AddListener(HandlePlasmaTrail);
        plasma2Btn = plasma2GameObject.GetComponent<Button>();
        plasma2Btn.onClick.AddListener(HandlePlasmaTrail);
        plasma3Btn = plasma3GameObject.GetComponent<Button>();
        plasma3Btn.onClick.AddListener(HandlePlasmaTrail);
    }

    public void OpenMenu()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        ActiveButtons();
    }

    void HandleMissileTrail()
    {
        UpdateTrailLine(TrailsType.MISSILE);
    }

    void HandleLaserTrail()
    {
        UpdateTrailLine(TrailsType.LASER);
    }

    void HandlePlasmaTrail()
    {
        UpdateTrailLine(TrailsType.PLASMA);
    }

    void UpdateTrailLine(TrailsType trailType)
    {
        player.BoxUpdate(trailType);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    void ColorBtn(Button btn)
    {
        var image = btn.GetComponent<Image>();
        image.color = Color.green;
    }

    void ActiveButtons()
    {
        missile1Btn.interactable = false;
        missile2Btn.interactable = false;
        missile3Btn.interactable = false;

        laser1Btn.interactable = false;
        laser2Btn.interactable = false;
        laser3Btn.interactable = false;

        plasma1Btn.interactable = false;
        plasma2Btn.interactable = false;
        plasma3Btn.interactable = false;

        if (player.trailsSum == 0)
        {
            missile1Btn.interactable = true;
            laser1Btn.interactable = true;
            plasma1Btn.interactable = true;
        }

        if (player.missileTrail == 0 && ((player.laserTrail > 0 && player.plasmaTrail == 0) || (player.laserTrail == 0 && player.plasmaTrail > 0)))
            missile1Btn.interactable = true;
        if (player.missileTrail == 1 && player.laserTrail < 2 && player.plasmaTrail < 2)
            missile2Btn.interactable = true;
        if (player.missileTrail == 2)
            missile3Btn.interactable = true;

        if (player.laserTrail == 0 && ((player.missileTrail > 0 && player.plasmaTrail == 0) || (player.missileTrail == 0 && player.plasmaTrail > 0)))
            laser1Btn.interactable = true;
        if (player.laserTrail == 1 && player.missileTrail < 2 && player.plasmaTrail < 2)
            laser2Btn.interactable = true;
        if (player.laserTrail == 2)
            laser3Btn.interactable = true;

        if (player.plasmaTrail == 0 && ((player.missileTrail > 0 && player.laserTrail == 0) || (player.missileTrail == 0 && player.laserTrail > 0)))
            plasma1Btn.interactable = true;
        if (player.plasmaTrail == 1 && player.missileTrail < 2 && player.laserTrail < 2)
            plasma2Btn.interactable = true;
        if (player.plasmaTrail == 2)
            plasma3Btn.interactable = true;

        if (player.missileTrail >= 1)
            ColorBtn(missile1Btn);
        if (player.missileTrail >= 2)
            ColorBtn(missile2Btn);
        if (player.missileTrail >= 3)
            ColorBtn(missile3Btn);

        if (player.laserTrail >= 1)
            ColorBtn(laser1Btn);
        if (player.laserTrail >= 2)
            ColorBtn(laser2Btn);
        if (player.laserTrail >= 3)
            ColorBtn(laser3Btn);

        if (player.plasmaTrail >= 1)
            ColorBtn(plasma1Btn);
        if (player.plasmaTrail >= 2)
            ColorBtn(plasma2Btn);
        if (player.plasmaTrail >= 3)
            ColorBtn(plasma3Btn);

        if (player.trailsSum >= 4)
            gameObject.SetActive(false);
    }

}
