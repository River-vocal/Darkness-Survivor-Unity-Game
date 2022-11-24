using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip jump, land, dash, attack, rangeAttack, injured, boss;
    [Range(0f, 2f)]
    public float jumpVolume, landVolume, dashVolume, attackVolume, rangeAttackVolume, injuredVolume, bossVolume;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        switch (name)
        {
            case "jump":
            {
                audioSource.volume = jumpVolume;
                audioSource.PlayOneShot(jump);
                break;
            }
            case "land":
            {
                audioSource.volume = landVolume;
                audioSource.PlayOneShot(land);
                break;
            }
            case "dash":
            {
                audioSource.volume = dashVolume;
                audioSource.PlayOneShot(dash);
                break;
            }
            case "attack":
            {
                audioSource.volume = attackVolume;
                audioSource.PlayOneShot(attack);
                break;
            }
            case "injured":
            {
                audioSource.volume = injuredVolume;
                audioSource.PlayOneShot(injured);
                break;
            }
            case "boss":
            {
                audioSource.volume = bossVolume;
                audioSource.PlayOneShot(boss);
                break;
            }
            case "rangeAttack":
            {
                audioSource.volume = rangeAttackVolume;
                audioSource.PlayOneShot(rangeAttack);
                break;
            }
        }
    }
}
