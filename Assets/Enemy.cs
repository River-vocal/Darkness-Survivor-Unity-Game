using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public Slider slider;
    public int maxHealth = 10;
    public int status = 0; // 0: normal, 1: on hit
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        switch (status) {
            case 0:
                gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 255, 100);
                break;
            case 1:
                gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 100);
                break;
        }
    }

    public void OnHit() {
        status = 1;
        slider.value -= 1;
        if (slider.value == 0){
            Destroy(gameObject);
            TimeCounter.enable = false;
        }
        StartCoroutine("BackToNormal");
    }

    IEnumerator BackToNormal () {
        yield return new WaitForSeconds(1);
        status = 0;
    }
}
