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
    // Start is called before the first frame update
    void Start()
    {
        // dc = GetComponent<DynamicLightController>();
        switchCycle = dc.lightSwitchIntervalTime;
        if (!initialActive)
        {
            platform.SetActive(false);
        }
        StartCoroutine(WaitToStart());
    }

    IEnumerator UpdatePlatform()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchCycle);
            platform.SetActive(!platform.activeInHierarchy);
        }
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(startTime);
        StartCoroutine(UpdatePlatform());
    }
}
