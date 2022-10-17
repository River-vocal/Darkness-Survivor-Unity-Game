using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData/BaseData")]
public class PlayerData : ScriptableObject
{
   [Header("Move State")] 
   public float movementVelocity = 10f;

   [Header("Jump State")]
   public float jumpVelocity = 15f;

   [Header("Check Variables")] 
   public float groundCheckDistance = 0.15f;
   public LayerMask groundLayer;
}
