using System;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] public GameObject healingOnce;
    [SerializeField] public GameObject healing;
    [SerializeField] public GameObject shield;
    [SerializeField] public float healOnceAnimationTime = 0.6f;

    private float timer;

    public void StartHealing()
    {
        healing.SetActive(true);
    }

    public void StopHealing()
    {
        healing.SetActive(false);
    }

    public void StartShield()
    {
        shield.SetActive(true);
    }

    public void StopShield()
    {
        shield.SetActive(false);
    }

    public void HealOnce()
    {
        healingOnce.SetActive(true);
        timer += healOnceAnimationTime;
    }

    private void Update()
    {
        if (timer <= 0) return;
        timer -= Time.deltaTime;
        if (timer <= 0) healingOnce.SetActive(false);
    }
}