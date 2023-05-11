using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

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
    [SerializeField] private AudioSource sfxSource;
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
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        PlayMusic(RandomClip());
    }

    public void SetSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        sfxSource.volume = value;
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        musicSource.volume = value;
    }

    public void FlingSFX() => sfxSource.PlayOneShot(flingSFX);
    public void SlapSFX() => sfxSource.PlayOneShot(slapSFX);
    public void CollectSFX() => sfxSource.PlayOneShot(collectSFX);

    public void ThrowSFX() => sfxSource.PlayOneShot(throwSFX);
    public void HitSFX() => sfxSource.PlayOneShot(hitSFX);

    public void BombSFX() => sfxSource.PlayOneShot(bombSFX);
    public void LowGravitySFX() => sfxSource.PlayOneShot(lowGravitySFX);
    public void FreezeSFX() => sfxSource.PlayOneShot(freezeSFX);

    public void InteractSFX() => sfxSource.PlayOneShot(interactSFX);

    public void GameOverSFX() => sfxSource.PlayOneShot(gameOverSFX);

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
