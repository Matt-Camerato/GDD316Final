using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
   private FlingController _playerController;
   private EffectManager _effectManager;
   
   public enum CurrentPickup
   {
      Bomb,
      LowGravity,
      None
   }

   public CurrentPickup pickupType;

   public int AmountOfFlings { get; set; }
   public float Force { get; set; }
   public float Duration { get; set; }

   private void Start()
   {
      TryGetComponent(out _playerController);
      TryGetComponent(out _effectManager);
      ChangeCurrentPickup(CurrentPickup.None);
   }

   private void Update()
   {
      if (!Input.GetKeyDown(KeyCode.Space)) return;
      switch (pickupType)
      {
         case CurrentPickup.Bomb:
            BombPickup();
            break;
         case CurrentPickup.LowGravity:
            LowGravityPickup();
            break;
         case CurrentPickup.None:
            break;
         default:
            throw new ArgumentOutOfRangeException();
      }
   }

   private void BombPickup()
   {
      _playerController.ApplyForce(_playerController.transform.forward, 2, Force);
      _effectManager.PlayEffect(EffectManager.Bomb);
      AudioManager.Instance.BombSFX();
      ChangeCurrentPickup(CurrentPickup.None);
   }

   public void AddFlingPickup(int numFlings)
   {
      _playerController.AddFlings(numFlings);
   }

   private void LowGravityPickup()
   {
      _playerController.ChangeGravity(FlingController.Gravity, Duration);
      AudioManager.Instance.LowGravitySFX();
   }

   public void ChangeCurrentPickup(CurrentPickup currentPickup)
   {
      pickupType = currentPickup;
      if(currentPickup == CurrentPickup.None) HUDManager.Instance.SetPowerupIcon(-1);
      else if(currentPickup == CurrentPickup.Bomb) HUDManager.Instance.SetPowerupIcon(0);
      else if(currentPickup == CurrentPickup.LowGravity) HUDManager.Instance.SetPowerupIcon(1);
   }

   public CurrentPickup CurrentPlayerPickup()
   {
      return pickupType;
   }
}
