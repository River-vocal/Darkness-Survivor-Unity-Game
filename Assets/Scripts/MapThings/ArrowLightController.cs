using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ArrowLightController : MonoBehaviour
{
    protected Light2D curLight;
    // Start is called before the first frame update
    void Start()
    {
        curLight = GetComponent<Light2D>();
        StartCoroutine(UpdateColor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UpdateColor()
    {
        while (true)
        {
            while (curLight.intensity > 0.1f)
            {
                curLight.intensity -= 0.02f;
                yield return new WaitForSeconds(0.05f);
            }
            while (curLight.intensity < 1f)
            {
                curLight.intensity += 0.02f;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
