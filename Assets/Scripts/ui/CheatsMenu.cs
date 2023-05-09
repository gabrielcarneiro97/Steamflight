using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CheatsMenu : MonoBehaviour
{
    public GameObject playerGameObject;
    public GameObject level1BtnGameObject;
    Button level1Btn;
    public GameObject level2BtnGameObject;
    Button level2Btn;
    public GameObject missileTrailBtnGameObject;
    Button missileTrailBtn;
    public GameObject laserTrailBtnGameObject;
    Button laserTrailBtn;
    public GameObject plasmaTrailBtnGameObject;
    Button plasmaTrailBtn;
    public GameObject resetTrailsBtnGameObject;
    Button resetTrailsBtn;
    public GameObject invencibleBtnGameObject;
    Button invencibleBtn;

    public GameObject trailsTextGameObject;
    TMP_Text trailsText;
    Player player;
    void Start()
    {
        player = playerGameObject.GetComponent<Player>();
        player.onTrailsChange.AddListener(HandleTrailsChange);

        level1Btn = level1BtnGameObject.GetComponent<Button>();
        level1Btn.onClick.AddListener(HandleLevel1);

        level2Btn = level2BtnGameObject.GetComponent<Button>();
        level2Btn.onClick.AddListener(HandleLevel2);

        missileTrailBtn = missileTrailBtnGameObject.GetComponent<Button>();
        missileTrailBtn.onClick.AddListener(HandleMissileTrail);

        laserTrailBtn = laserTrailBtnGameObject.GetComponent<Button>();
        laserTrailBtn.onClick.AddListener(HandleLaserTrail);

        plasmaTrailBtn = plasmaTrailBtnGameObject.GetComponent<Button>();
        plasmaTrailBtn.onClick.AddListener(HandlePlasmaTrail);

        resetTrailsBtn = resetTrailsBtnGameObject.GetComponent<Button>();
        resetTrailsBtn.onClick.AddListener(HandleResetTrails);

        invencibleBtn = invencibleBtnGameObject.GetComponent<Button>();
        invencibleBtn.onClick.AddListener(HandleInvencible);

        trailsText = trailsTextGameObject.GetComponent<TMP_Text>();
        SetText();
    }

    public void HandleLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void HandleLevel2()
    {
        SceneManager.LoadScene(2);
    }

    public void HandleMissileTrail()
    {
        player.BoxUpdate("MissileCannon");
    }

    public void HandleLaserTrail()
    {
        player.BoxUpdate("LaserCannon");
    }

    public void HandlePlasmaTrail()
    {
        player.BoxUpdate("PlasmaCannon");
    }

    void SetText()
    {
        trailsText.text = $"Missile: {player.missileTrail}\t\tLaser: {player.laserTrail}\nPlasma: {player.plasmaTrail}\t\tSum: {player.trailsSum}";
    }

    public void HandleResetTrails()
    {
        player.ResetTrails();

        missileTrailBtn.interactable = true;
        laserTrailBtn.interactable = true;
        plasmaTrailBtn.interactable = true;
        SetText();
    }

    public void HandleTrailsChange()
    {
        if (player.laserTrail >= 1 && player.plasmaTrail >= 1) missileTrailBtn.interactable = false;
        if (player.trailsSum >= 3 && player.missileTrail == 1) missileTrailBtn.interactable = false;
        if (player.missileTrail >= 3) missileTrailBtn.interactable = false;

        if (player.missileTrail >= 1 && player.plasmaTrail >= 1) laserTrailBtn.interactable = false;
        if (player.trailsSum >= 3 && player.laserTrail == 1) laserTrailBtn.interactable = false;
        if (player.laserTrail >= 3) laserTrailBtn.interactable = false;

        if (player.missileTrail >= 1 && player.laserTrail >= 1) plasmaTrailBtn.interactable = false;
        if (player.trailsSum >= 3 && player.plasmaTrail == 1) plasmaTrailBtn.interactable = false;
        if (player.plasmaTrail >= 3) plasmaTrailBtn.interactable = false;

        if (player.trailsSum >= 4)
        {
            missileTrailBtn.interactable = false;
            laserTrailBtn.interactable = false;
            plasmaTrailBtn.interactable = false;
        }

        SetText();
    }

    public void HandleInvencible()
    {
        player.invencible = !player.invencible;

        var image = invencibleBtn.GetComponent<Image>();
        image.color = player.invencible ? Color.green : Color.white;
    }

}
