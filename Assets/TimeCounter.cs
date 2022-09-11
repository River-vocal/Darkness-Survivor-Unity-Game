using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 30;
        
    // Start is called before the first frame update
    void Start()
    {
        textDisplay.GetComponent<TMP_Text>().text = "Time left:" + secondsLeft;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (!takingAway && secondsLeft > 0)
    //     {
    //         StartCoroutine(TimerTake());
    //     }
    // }

    private void FixedUpdate()
    {
        int timePassed = (int) MathF.Floor(Time.time);
        int curLeftSec = secondsLeft - timePassed;
        textDisplay.GetComponent<TMP_Text>().text = "Time left:" + curLeftSec;
    }

    // IEnumerator TimerTake()
    // {
    //     takingAway = true;
    //     yield return new WaitForSeconds(1);
    //     secondsLeft -= 1;
    //     textDisplay.GetComponent<TMP_Text>().text = "Time left:" + secondsLeft;
    //     takingAway = false;
    // }
}
