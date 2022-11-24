using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip move, jump, land, dash, attack, injured, background, boss;

    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        switch (name)
        {
            case "move":
                audioSource.PlayOneShot(move);
                break;
            case "jump":
                audioSource.PlayOneShot(jump);
                break;
            case "land":
                audioSource.PlayOneShot(land);
                break;
            case "dash":
                audioSource.PlayOneShot(dash);
                break;
            case "attack":
                audioSource.PlayOneShot(attack);
                break;
            case "injured":
                audioSource.PlayOneShot(injured);
                break;
            case "background":
                audioSource.PlayOneShot(background);
                break;
            case "boss":
                audioSource.PlayOneShot(boss);
                break;
        }
    }
}
