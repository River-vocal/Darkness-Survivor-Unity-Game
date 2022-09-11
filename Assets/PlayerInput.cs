using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Animator animator;

    private Rigidbody2D rb;
    
    public float moveSpeed = 5;

    // public Transform movePoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        
        float moveX = Input.GetAxisRaw(PAP.axisXinput);
        
        animator.SetFloat(PAP.moveX, moveX);
        
        bool isMoving = !Mathf.Approximately(moveX, 0f);
        
        animator.SetBool(PAP.isMoving, isMoving);

        // if (GlobalTimer.ableToMove){
        //     transform.position += new Vector3(1, 0, 0);
        // }


    }

    private void FixedUpdate()
    {
        int millSec = GlobalTimer.millSec;
        bool ableToMove = (millSec<100 || millSec > 900);
        if(!ableToMove){
            return;
        }
        float forceX = animator.GetFloat(PAP.forceX);
        if(forceX != 0) rb.AddForce(new Vector2(forceX, 0));
        
        float impulseY = animator.GetFloat(PAP.impulseY);
        if(impulseY != 0) rb.AddForce(new Vector2(0, impulseY), ForceMode2D.Impulse);
        
    }
}
