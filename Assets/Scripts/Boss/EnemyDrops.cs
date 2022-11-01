using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] public int damage;
    [SerializeField] public Boolean hasTwoLives;
    [SerializeField] public bool isRoundWalk;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    private Animator anim;
    private Boolean beAttacked = false;
    private string direction = "right";
    public Transform groundCheck;
    private float width;
    private float height;
    private SpriteRenderer sp;


    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        anim = GetComponent<Animator>();
        width = GetComponent<Collider2D>().bounds.size.x;
        height = GetComponent<Collider2D>().bounds.size.y;
        sp = GetComponent<SpriteRenderer>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckDistance);
        Gizmos.DrawLine(transform.position,
            new Vector3(transform.position.x + movementDistance, transform.position.y, transform.position.z));
        Gizmos.DrawLine(transform.position,
            new Vector3(transform.position.x - movementDistance, transform.position.y, transform.position.z));
    }

    private void Update()
    {
        if (isRoundWalk)
        {
            bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckDistance, groundLayer);
            if (!isGrounded)
            {
                switch (direction)
                {
                    // current moving right 
                    case "right":
                        transform.Rotate(0, 0, -90);
                        transform.position += new Vector3(height / 2, -width, 0);
                        direction = "down";
                        break;
                    case "down":

                        transform.Rotate(0, 0, -90);
                        transform.position += new Vector3(-width, -height / 2, 0);
                        direction = "left";
                        break;
                    case "left":
                        transform.Rotate(0, 0, -90);
                        transform.position += new Vector3(-height / 2, width, 0);
                        direction = "up";
                        break;
                    case "up":

                        transform.Rotate(0, 0, -90);
                        transform.position += new Vector3(width, height / 2, 0);
                        direction = "right";
                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    // current moving right 
                    case "right":
                        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                        break;
                    case "down":
                        transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
                        break;
                    case "left":
                        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
                        break;
                    case "up":
                        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                        break;
                }
            }
        }
        else
        {
            if (movingLeft)
            {
                if (transform.position.x > leftEdge)
                {
                    transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    movingLeft = false;
                    sp.flipX = false;
                }
            }
            else
            {
                if (transform.position.x < rightEdge)
                {
                    transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    movingLeft = true;
                    sp.flipX = true;
                }
            }
        }
    }


    public void DropDeath()
    {
        if (!hasTwoLives || beAttacked)
        {
            speed = 0;
            anim.SetTrigger("Death");
        }
        else
        {
            beAttacked = true;

            transform.localScale = new Vector3(transform.localScale.x * 0.7f, transform.localScale.y * 0.7f,
                transform.localScale.z * 0.7f);
            transform.position += new Vector3(0, -height * 0.4f, 0);
            speed *= 1.5f;

            anim.SetTrigger("Hurt");
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.GetComponent<Energy>().CurEnergy -= damage;
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}