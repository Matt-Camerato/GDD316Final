using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Slider sfxSlider, musicSlider;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        //set initial slider values
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
    }

    public void StartGame() => SceneManager.LoadScene(1);

    public void Settings() => anim.SetTrigger("Settings");
    public void SetSFXVolume(float value) => AudioManager.Instance.SetSFXVolume(value);
    public void SetMusicVolume(float value) => AudioManager.Instance.SetMusicVolume(value);
    public void Return() => anim.SetTrigger("Return");

    public void InteractSFX() => AudioManager.Instance.InteractSFX();
}
