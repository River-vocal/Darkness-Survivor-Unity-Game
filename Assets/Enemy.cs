using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int status = 0; // 0: normal, 1: on hit
    // Start is called before the first frame update
    void Start()
    {
        
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
        StartCoroutine("BackToNormal");
    }

    IEnumerator BackToNormal () {
        yield return new WaitForSeconds(1);
        status = 0;
    }
}
