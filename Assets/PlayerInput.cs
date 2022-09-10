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

    public Transform movePoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        
        float moveX = Input.GetAxisRaw(PAP.axisXinput);
        
        animator.SetFloat(PAP.moveX, moveX);
        
        bool isMoving = !Mathf.Approximately(moveX, 0f);
        
        animator.SetBool(PAP.isMoving, isMoving);

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Math.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }
        }
        
        
        

    }

    private void FixedUpdate()
    {
        float forceX = animator.GetFloat(PAP.forceX);
        if(forceX != 0) rb.AddForce(new Vector2(forceX, 0));
        
        float impulseY = animator.GetFloat(PAP.impulseY);
        if(impulseY != 0) rb.AddForce(new Vector2(0, impulseY), ForceMode2D.Impulse);
        
    }
}
