using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageZone : MonoBehaviour
{
    public GameObject boss = null;
    [TextArea] [SerializeField] public String message = "Empty Message";
    [SerializeField] public float displayMinimumTime = 3f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        if (boss != null)
        {
            if (boss.activeSelf)
            {
                HintArea.Hint(message, displayMinimumTime);
            }
        }
        else
        {
            HintArea.Hint(message, displayMinimumTime);
        }
    }
}