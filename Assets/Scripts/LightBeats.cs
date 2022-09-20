using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBeats : MonoBehaviour
{
    public Light2D spotLight;
    public SwordAttack swordAttack;
    public AudioSource audioSource;
    private float time = 3f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
        time -= Time.fixedDeltaTime;
        if (time <= 0) {
            if (spotLight.color == Color.white) {
                spotLight.color = Color.red;
                time = 1.0f;
                swordAttack.damage = 40;
            }
            else {
                spotLight.color = Color.white;
                time = 3f;
                swordAttack.damage = 10;
            }
        }
    }
}
