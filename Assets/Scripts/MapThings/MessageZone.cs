using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageZone : MonoBehaviour
{
    [TextArea] [SerializeField] public String message = "Empty Message";
    [SerializeField] public float displayMinimumTime = 3f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        HintArea.Hint(message, displayMinimumTime);
    }
}