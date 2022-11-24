using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private Transform cam;

    private Vector3 camStartPos;

    private float distance;

    private GameObject[] backgrounds;

    private Material[] mat;

    private float[] backSpeed;

    private float farthestBack;

    private bool stopFollowing = false;
    
    [Range(0.01f, 0.05f)] public float parallaxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void Update()
    {
        if (!stopFollowing)
        {
            distance = cam.position.x - camStartPos.x;
            transform.position = new Vector3(cam.position.x + 10, cam.position.y - 5, 100);
            for (int i = 0; i < backgrounds.Length; i++)
            {
                float speed = backSpeed[i] * parallaxSpeed;
                mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
            }
        }
    }

    public void StopFollowing()
    {
        stopFollowing = true;
    }

    public void ResetFollowing()
    {
        stopFollowing = false;
    }
}
