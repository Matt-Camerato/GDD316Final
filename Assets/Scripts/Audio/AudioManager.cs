using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public static float SFXVolume;
    public static float MusicVolume;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip flingSFX;
    [SerializeField] private AudioClip slapSFX;
    [SerializeField] private AudioClip collectSFX;
    [SerializeField] private AudioClip throwSFX, hitSFX;
    [SerializeField] private AudioClip bombSFX, lowGravitySFX, freezeSFX;
    [SerializeField] private AudioClip interactSFX;
    [SerializeField] private AudioClip gameOverSFX;
    [SerializeField] private AudioClip[] music;
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource musicSource;
   
    private int _lastSong;
    
    private void Awake()
    {
        //set singleton instance
        if(Instance == null) Instance = this;
        else Destroy(gameObject);

        //make audio manager persist across scenes
        DontDestroyOnLoad(gameObject);

        //load and set volume levels
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.15f);
        musicSource.volume = MusicVolume;
        SFXSource.volume = SFXVolume; 
        PlayMusic(RandomClip());
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

    private void PlayMusic(int i)
    {
        musicSource.clip = music[i];
        musicSource.Play();
        StartCoroutine(WaitForSongToEnd());
    }

    private static IEnumerator WaitForSongToEnd()
    {
        while(Instance.musicSource.isPlaying)
        {
            yield return null;
        }

        var i = Instance.RandomClip();
        Instance._lastSong = i;
        Instance.PlayMusic(i);
    }

    private int RandomClip()
    {
        var i = Random.Range(0, music.Length);
        i = i == _lastSong ? RandomClip() : i;
        return i;
    }
}
