using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    
    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private TMP_Text flingCountText;

    [SerializeField] private Transform playerTransform;

    private void Awake() => Instance = this;

    private void Update()
    {
        //update distance text with player's x position
        distanceText.text = "Distance: " + playerTransform.position.x.ToString("F2") + "m";
    }

    public void UpdateFlingCount(int count) => flingCountText.text = count.ToString();
}
