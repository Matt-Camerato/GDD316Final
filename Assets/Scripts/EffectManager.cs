using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem frozenEffect;
    [SerializeField] private ParticleSystem rockEffect;
    [SerializeField] private ParticleSystem bombEffect;
    [SerializeField] private bool playEffectDebug;
    public const string Freeze = "Freeze";
    public const string Rock = "Rock";
    public const string Bomb = "Bomb";
    
    private void Update()
    {
        if (playEffectDebug)
        {
            StartCoroutine(PlayEffect(frozenEffect));
        }
    }

    private IEnumerator PlayEffect(ParticleSystem effect)
    {
        var main = effect.main;
        var duration = main.duration;
        playEffectDebug = false;
        effect.Play();
        //Debug.Log("Play");
        yield return new WaitForSeconds(duration/ 2);
        effect.Pause();
        //Debug.Log("Pause");
        yield return new WaitForSeconds(duration / 2);
        //Debug.Log("Resume");
        effect.Play();
    }
    
    public void ShowEffect(string effectName)
    {
        switch (effectName)
        {
            case "Freeze":
                StartCoroutine(PlayEffect(frozenEffect));
                break;
            case "Rock":
                StartCoroutine(PlayEffect(rockEffect));
                break;
            case "Bomb":
                StartCoroutine(PlayEffect(bombEffect));
                break;
        }
    }

    
}
