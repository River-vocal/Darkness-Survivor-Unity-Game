using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip move, jump, land, dash, attack, injured, boss;

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
            case "move":
            {
                audioSource.PlayOneShot(move);
                break;
            }
            case "jump":
            {
                audioSource.PlayOneShot(jump);
                break;
            }
            case "land":
            {
                audioSource.PlayOneShot(land);
                break;
            }
            case "dash":
            {
                audioSource.PlayOneShot(dash);
                break;
            }
            case "attack":
            {
                audioSource.PlayOneShot(attack);
                break;
            }
            case "injured":
            {
                audioSource.PlayOneShot(injured);
                break;
            }
            case "boss":
            {
                audioSource.PlayOneShot(boss);
                break;
            }
        }
    }
}
