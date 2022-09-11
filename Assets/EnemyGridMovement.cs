using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGridMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isMoving;
    private Vector3 originPos, targetPos;
    private float timeToMove = 0.2f;
    private float leftXPos, rightXPos;
    private bool isMoveRight;
    private float originX; 

    private Vector3 playerPos;

    private bool hasMoved;

    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        leftXPos = transform.position.x - 1;
        rightXPos = transform.position.x + 1;
        isMoveRight = true;

    }

    // Update is called once per frame
    void Update()
    {

        int millSec = GlobalTimer.millSec;
        if (millSec < 100 || millSec > 900) 
        {
            if (!hasMoved)
            {
                if (isMoveRight)
                {
                    MoveEnemy(Vector3.right);
                }

                else
                {
                    MoveEnemy(Vector3.left);
                }

                hasMoved = true;
                

            }
        }
        else
        {
            hasMoved = false;
        }

    }
    

    private void MoveEnemy(Vector3 direction)
    {

        isMoving = true;

        float elapsedTime = 0f;
        
        originPos = transform.position;
        targetPos = originPos + direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
        }
        
        
        transform.position = targetPos;

        isMoving = false;
        
        if (transform.position.x <= leftXPos || transform.position.x >= rightXPos)
        {
            isMoveRight = !isMoveRight;
        }
    }
}
