using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDisappearController : MonoBehaviour
{
    public float startTime;
    [SerializeField] private GameObject platform;
    private float switchCycle;

    [SerializeField] private DynamicLightController dc;

    [SerializeField] private bool initialActive;
    public float warningTime = 1f;

    private int state; // 0: stable, 1: fade, 2: disappear

    private float timer;

    private SpriteRenderer renderer;

    private Vector3 originalPlatformPosition;
    private Color originalRenderColor;

    // Start is called before the first frame update
    void Start()
    {
        // dc = GetComponent<DynamicLightController>();
        switchCycle = dc.lightSwitchIntervalTime;
        if (!initialActive)
        {
            platform.SetActive(false);
            state = 2;
        }
        else
        {
            state = 0;
        }

        timer = switchCycle;
        renderer = platform.GetComponent<SpriteRenderer>();
        originalPlatformPosition = platform.transform.position;
        originalRenderColor = renderer.color;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        switch (state)
        {
            case 0:
                platform.SetActive(true);
                renderer.color = originalRenderColor;
                if (timer < warningTime) state = 1;
                break;
            case 1:
                // fade the platform
                float alpha = 0.1f + timer / warningTime;
                Color color = renderer.color;
                color.a = alpha;
                renderer.color = color;

                // shake the platform
                platform.transform.position =
                    originalPlatformPosition + new Vector3(Mathf.Sin(Time.time * 10f) * 0.1f, 0, 0);

                // check enter next state
                if (timer < 0f)
                {
                    state = 2;
                    timer = switchCycle;
                }

                break;
            case 2:
                platform.SetActive(false);
                if (timer < 0f)
                {
                    state = 0;
                    timer = switchCycle;
                }

                break;
        }
    }
    
}