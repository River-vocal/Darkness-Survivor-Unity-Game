using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    // Start is called before the first frame update
    
    //https://www.youtube.com/watch?v=AiZ4z4qKy44

    private bool isMoving;
    private Vector3 originPos, targetPos;
    private float timeToMove = 0.2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKey(KeyCode.W) && !isMoving)
        // {
        //     
        // }

        if (Input.GetKey(KeyCode.A) && !isMoving)
        {
            StartCoroutine(MovePlayer(Vector3.left));
        }

        // if (Input.GetKey(KeyCode.S) && !isMoving)
        // {
        //     
        // }

        if (Input.GetKey(KeyCode.D) && !isMoving)
        { 
            StartCoroutine(MovePlayer(Vector3.right));
        }

    }
    

    private IEnumerator MovePlayer(Vector3 direction)
    {

        isMoving = true;

        float elapsedTime = 0;
        
        originPos = transform.position;
        targetPos = originPos + direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }
}
