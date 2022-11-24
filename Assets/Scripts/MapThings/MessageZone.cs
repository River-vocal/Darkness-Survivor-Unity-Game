using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageZone : MonoBehaviour
{
    [TextArea] [SerializeField] public String Message_en = "Empty Message";
    [TextArea][SerializeField] public String Message_cn = "Empty Message";
    [TextArea][SerializeField] public String Message_jp = "Empty Message";
    [SerializeField] public float displayMinimumTime = 3f;
    public TMP_FontAsset m_fontAsset;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        switch (ID)
        {
            case 0:
                HintArea.Hint(Message_en, displayMinimumTime, m_fontAsset);
                break;
            case 1:
                HintArea.Hint(Message_cn, displayMinimumTime, m_fontAsset);
                break;
            case 2:
                HintArea.Hint(Message_jp, displayMinimumTime, m_fontAsset);
                break;
            default:
                HintArea.Hint(Message_en, displayMinimumTime, m_fontAsset);
                break;
        }
    }
}