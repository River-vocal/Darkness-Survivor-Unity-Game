using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChanger : MonoBehaviour
{
    [SerializeField] public PlayerData playerData;
    [SerializeField] public GameObject speedEffectParticleSystem;
    private float timer;
    private float originMoveSpeed;
    private float originDashSpeed;

    private void Awake()
    {
        originMoveSpeed = playerData.movementVelocity;
        originDashSpeed = playerData.dashVelocity;
    }

    public void SpeedUp(float factor, float time)
    {
        timer += time;
        playerData.movementVelocity = originMoveSpeed * factor;
        playerData.dashVelocity = originDashSpeed * factor;
        speedEffectParticleSystem.SetActive(true);
    }

    private void Update()
    {
        if (timer <= 0) return;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            playerData.movementVelocity = originMoveSpeed;
            playerData.dashVelocity = originDashSpeed;
            speedEffectParticleSystem.SetActive(false);
        }
    }

    private void OnDisable()
    {
        playerData.movementVelocity = originMoveSpeed;
        playerData.dashVelocity = originDashSpeed;
    }
}