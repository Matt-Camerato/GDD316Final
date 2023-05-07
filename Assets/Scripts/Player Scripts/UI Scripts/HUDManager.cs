using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    
    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private TMP_Text flingCountText;
    [SerializeField] private Image powerupIcon;
    [SerializeField] private List<Sprite> powerupSprites = new List<Sprite>();
    [SerializeField] private Image powerupDuration;

    [SerializeField] private Transform playerTransform;

    private void Awake() => Instance = this;

    private void Update()
    {
        //update distance text with player's x position
        distanceText.text = "Distance: " + playerTransform.position.x.ToString("F2") + "m";
    }

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
}
