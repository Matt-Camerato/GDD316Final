using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    public static event Action ActivatePowerUp;
    
    public bool IsPaused = false;
    
    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private TMP_Text flingCountText;
    [SerializeField] private Image powerupIcon;
    [SerializeField] private List<Sprite> powerupSprites = new List<Sprite>();
    [SerializeField] private Image powerupDuration;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider sfxSlider, musicSlider;
    [SerializeField] private Transform playerTransform;

    private void Awake() => Instance = this;

    private void Update()
    {
        //update distance text with player's x position
        distanceText.text = "Distance: " + playerTransform.position.x.ToString("F2") + "m";

        if(Input.GetKeyDown(KeyCode.Escape)) ToggleSettings();
    }

    public void ToggleSettings()
    {
        if(settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
            IsPaused = false;
            return;
        }

        settingsPanel.SetActive(true);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        IsPaused = true;
    }

    public void SetSFXVolume(float value) => AudioManager.Instance.SetSFXVolume(value);
    public void SetMusicVolume(float value) => AudioManager.Instance.SetMusicVolume(value);

    public void UpdateFlingCount(int count) => flingCountText.text = count.ToString();

    public void SetPowerupIcon(int index)
    {
        if(index == -1)
        {
            powerupIcon.enabled = false;
            powerupDuration.fillAmount = 0;
            return;
        }

        powerupIcon.enabled = true;
        powerupDuration.fillAmount = 1;
        powerupIcon.sprite = powerupSprites[index];
    }

    public void SetDuration(float value, float duration) => powerupDuration.fillAmount = value / duration;

    //game over screen methods
    public void PlayAgain() => SceneManager.LoadScene(1);
    public void Quit() => SceneManager.LoadScene(0);

    public void PlayerPowerUp()
    {
        ActivatePowerUp?.Invoke();
    }
    //button SFX method
    public void InteractSFX() => AudioManager.Instance.InteractSFX();
}
