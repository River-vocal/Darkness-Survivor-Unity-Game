using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDisappearController : MonoBehaviour
{
    public float startTime;
    [SerializeField] private GameObject platform;
    private float switchCycle;

    [SerializeField] private DynamicLightController dc;
    // Start is called before the first frame update
    void Start()
    {
        // dc = GetComponent<DynamicLightController>();
        switchCycle = dc.lightSwitchIntervalTime;
        StartCoroutine(WaitToStart());
        StartCoroutine(UpdatePlatform());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UpdatePlatform()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchCycle);
            if (platform.activeInHierarchy)
            {
                platform.SetActive(false);
            }
            else
            {
                platform.SetActive(true);
            }
        }
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(startTime);
    }
}
