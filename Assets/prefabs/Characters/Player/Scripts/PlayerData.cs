using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData/BaseData")]
public class PlayerData : ScriptableObject
{
   [Header("Move State")] 
   public float movementVelocity = 10f;

   [Header("Jump State")]
   public float jumpVelocity = 20f;
   public int jumpTimes = 2;

   [Header("In Air State")] 
   public float coyoteTime = 0.2f;
   public float variableJumpHeightMultiplier = 0.99f;

   [Header("Wall Slide State")] 
   public float wallSlideVelocity = 3f;

   [Header("Wall Jump State")] 
   public float wallJumpVelocity = 20f;
   public Vector2 wallJumpAngle = new Vector2(1, 2);

   [Header("Dash State")] 
   public float dashCoolDown = 0.1f;
   public float dashVelocity = 30f;
   public float dashDrag = 3f;
   public float dashYVelocityMultiplier = 0.15f;

   [Header("Attack State")] 
   public float attackMovementDrag = 3f;
   public float attackGravityScale = 2f;
   public int attackDamage = 10;

   [Header("Kouchoku State")] 
   public float invulnerableTime = 1f;
   public float blinkInterval = 0.1f;
   

   [Header("Check Variables")] 
   public float groundCheckDistance = 0.15f;
   public float wallCheckDistance = 0.3f;
   public float backWallCheckDistance = 1f;
   public float attackCheckDistance = 0.8f;
   public LayerMask groundLayer;
   public LayerMask wallLayer;
   public LayerMask enemyLayer;
   
   [Header("range Attack")]
   public int playerInitialBulletCount = 70;
}
