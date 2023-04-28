using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
   private FlingController _playerController;
   
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
      ChangeCurrentPickup(CurrentPickup.None);
   }

   public void AddFlingPickup(int numFlings)
   {
      _playerController.AddFlings(numFlings);
      ChangeCurrentPickup(CurrentPickup.None);
   }

   private void LowGravityPickup()
   {
      _playerController.ChangeGravity(FlingController.Gravity, Duration);
   }

   public void ChangeCurrentPickup(CurrentPickup currentPickup)
   {
      pickupType = currentPickup;
   }

   public CurrentPickup CurrentPlayerPickup()
   {
      return pickupType;
   }
}
