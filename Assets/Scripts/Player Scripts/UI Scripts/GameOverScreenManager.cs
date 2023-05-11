using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreenManager : MonoBehaviour
{   
    [Header("Dev Options")]
    [SerializeField] private bool resetBestDistances;

    [Header("References")]
    [SerializeField] private Animator HUDAnimator;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private TMP_Text newRecordText;

    [SerializeField] private TMP_Text BestDistance1Text, BestDistance2Text, BestDistance3Text;

    //set all values on awake
    private void Awake()
    {
        //set distance text
        float distance = playerTransform.position.x;
        distanceText.text = "Distance: " + distance.ToString("F2") + "m";

        //check if distance was a new record
        if(isBestDistance(distance))
        {
            //if so, turn on new record text and update best distances
            newRecordText.enabled = true;
            UpdateBestDistances(distance);
        }
        else newRecordText.enabled = false;

        //set best distances text
        if(PlayerPrefs.GetFloat("BestDistance1", 0) == 0) BestDistance1Text.enabled = false;
        else BestDistance1Text.text = "1.) " + PlayerPrefs.GetFloat("BestDistance1").ToString("F2") + "m";
        if(PlayerPrefs.GetFloat("BestDistance2", 0) == 0) BestDistance2Text.enabled = false;
        else BestDistance2Text.text = "2.) " + PlayerPrefs.GetFloat("BestDistance2").ToString("F2") + "m";
        if(PlayerPrefs.GetFloat("BestDistance3", 0) == 0) BestDistance3Text.enabled = false;
        else BestDistance3Text.text = "3.) " + PlayerPrefs.GetFloat("BestDistance3").ToString("F2") + "m";
    }

    private bool isBestDistance(float distance)
    {
        if(PlayerPrefs.GetFloat("BestDistance1", 0) < distance) return true;
        if(PlayerPrefs.GetFloat("BestDistance2", 0) < distance) return true;
        if(PlayerPrefs.GetFloat("BestDistance3", 0) < distance) return true;
        return false;
    }

    private void UpdateBestDistances(float distance)
    {
        //get previous records if possible
        float distance1 = PlayerPrefs.GetFloat("BestDistance1", 0);
        float distance2 = PlayerPrefs.GetFloat("BestDistance2", 0);
        float distance3 = PlayerPrefs.GetFloat("BestDistance3", 0);

        //update records with new distance
        if(distance1 < distance)
        {
            //if new top distance, update player prefs accordingly
            PlayerPrefs.SetFloat("BestDistance1", distance);
            if(distance1 != 0) PlayerPrefs.SetFloat("BestDistance2", distance1);
            if(distance2 != 0) PlayerPrefs.SetFloat("BestDistance3", distance2);
        }
        else if(distance2 < distance)
        {
            //if new 2nd place distance, update player prefs accordingly
            PlayerPrefs.SetFloat("BestDistance2", distance);
            if(distance2 != 0) PlayerPrefs.SetFloat("BestDistance3", distance2);
        }
        else
        {
            //otherwise this is a new 3rd place distance
            PlayerPrefs.SetFloat("BestDistance3", distance);
        }
    }

    private void Update()
    {
        //if dev option has been selected, reset best distances
        if(resetBestDistances)
        {
            PlayerPrefs.SetFloat("BestDistance1", 0);
            PlayerPrefs.SetFloat("BestDistance2", 0);
            PlayerPrefs.SetFloat("BestDistance3", 0);
            resetBestDistances = false;
        }
    }

    //button methods cue screen fade into scene transition
    public void PlayAgainButton() => HUDAnimator.SetTrigger("PlayAgain");
    public void QuitButton() => HUDAnimator.SetTrigger("Quit");
}
