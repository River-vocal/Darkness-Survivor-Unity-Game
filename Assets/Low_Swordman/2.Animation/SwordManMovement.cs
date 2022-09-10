using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D rigidbody2d;

    public Transform movePoint;

    public float kVelocity = 10f;

    public float moveForce = 500f;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        if (rigidbody2d.bodyType == RigidbodyType2D.Kinematic)
        {
            rigidbody2d.MovePosition(rigidbody2d.position + moveDirection * moveForce * Time.deltaTime);
        }
        else if(rigidbody2d.bodyType == RigidbodyType2D.Dynamic)
        {
            rigidbody2d.AddForce(moveDirection * moveForce * Time.deltaTime);
        }

    }
}
