using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public static float SFXVolume;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip flingSFX;
    [SerializeField] private AudioClip slapSFX;
    [SerializeField] private AudioClip collectSFX;
    [SerializeField] private AudioClip throwSFX, hitSFX;
    [SerializeField] private AudioClip bombSFX, lowGravitySFX, freezeSFX;
    [SerializeField] private AudioClip interactSFX;
    [SerializeField] private AudioClip gameOverSFX;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        //set singleton instance
        if(Instance == null) Instance = this;
        else Destroy(gameObject);

        //make audio manager persist across scenes
        DontDestroyOnLoad(gameObject);

        //load and set volume levels
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        SFXSource.volume = SFXVolume;
        //remember to set slider value
    }

    public void FlingSFX() => SFXSource.PlayOneShot(flingSFX);
    public void SlapSFX() => SFXSource.PlayOneShot(slapSFX);
    public void CollectSFX() => SFXSource.PlayOneShot(collectSFX);

    public void ThrowSFX() => SFXSource.PlayOneShot(throwSFX);
    public void HitSFX() => SFXSource.PlayOneShot(hitSFX);

    public void BombSFX() => SFXSource.PlayOneShot(bombSFX);
    public void LowGravitySFX() => SFXSource.PlayOneShot(lowGravitySFX);
    public void FreezeSFX() => SFXSource.PlayOneShot(freezeSFX);

    public void InteractSFX() => SFXSource.PlayOneShot(interactSFX);

    public void GameOverSFX() => SFXSource.PlayOneShot(gameOverSFX);
}
