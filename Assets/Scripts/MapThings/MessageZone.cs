using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageZone : MonoBehaviour
{
    [SerializeField] public String message = "Empty Message";
    [SerializeField] public float displayMinimumTime = 3f;

    private float timer;
    private bool display;

    private void Awake()
    {
        timer = displayMinimumTime;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (display) HintArea.Show();
            if (timer <= 0 && !display) HintArea.Hide();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        HintArea.SetText(message);
        display = true;
        timer = displayMinimumTime;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        display = false;
    }
}