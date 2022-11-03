using System;
using TMPro;
using UnityEngine;

public class HintArea : MonoBehaviour
{
    public float fadeSpeed = 0.1f;
    
    private static TMP_Text tmpText;
    private static bool enable;

    private float originAlpha;
    private float alpha;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
        originAlpha = tmpText.alpha;
        tmpText.alpha = 0;
    }

    private void Update()
    {
        if (enable && originAlpha - alpha > 0.01)
        {
            alpha = Mathf.MoveTowards(alpha, originAlpha, fadeSpeed*Time.deltaTime);
            tmpText.alpha = alpha;
        }else if (!enable && alpha > 0.01)
        {
            alpha = Mathf.MoveTowards(alpha, 0, fadeSpeed*Time.deltaTime);
            tmpText.alpha = alpha;
        }
    }

    public static void SetText(String text)
    {
        tmpText.SetText(text);
    }

    public static void Show()
    {
        enable = true;
    }

    public static void Hide()
    {
        enable = false;
    }
}
