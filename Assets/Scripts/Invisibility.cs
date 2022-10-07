using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    private SpriteRenderer Boss;
    private Color col;
    private float activationTime;
    private bool invisible;
    // Start is called before the first frame update
    void Start()
    {
        Boss = GetComponent<SpriteRenderer>();
        activationTime = 0;
        invisible = false; 
        col = Boss.color;
    }

    // Update is called once per frame
    void Update()
    {
        activationTime += Time.deltaTime;
        if (invisible && activationTime >= 3)
        {
            invisible = false;
            col.a = 1;
            Boss.color = col;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "invisible");
        {
            invisible = true;
            activationTime = 0;
            col.a = .2f;
            Boss.color = col;
        }
    }
}
