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

    private static float timer;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
        originAlpha = tmpText.alpha;
        tmpText.alpha = 0;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (!enable) enable = true;
            if (timer <= 0) enable = false;
        }

        if (enable && originAlpha - alpha > 0.01)
        {
            alpha = Mathf.MoveTowards(alpha, originAlpha, fadeSpeed * Time.deltaTime);
            tmpText.alpha = alpha;
        }
        else if (!enable && alpha > 0.01)
        {
            alpha = Mathf.MoveTowards(alpha, 0, fadeSpeed * Time.deltaTime);
            tmpText.alpha = alpha;
        }
    }

    public static void Hint(String text, float minTime, TMP_FontAsset m_fontAsset)
    {
        tmpText.SetText(text);
        tmpText.font = m_fontAsset;
        timer = minTime;
    }
}