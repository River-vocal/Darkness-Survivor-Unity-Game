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

        int millSec = GlobalTimer.millSec;
        bool ableToMove = (millSec<100 || millSec > 900);

        if (Input.GetKey(KeyCode.A) && !isMoving && ableToMove )
        {
            StartCoroutine(MovePlayer(Vector3.left));
            Debug.Log(millSec);
        }

        if (Input.GetKey(KeyCode.D) && !isMoving  && ableToMove)
        { 
            StartCoroutine(MovePlayer(Vector3.right));
            Debug.Log(millSec);
        }

    }
    

    private IEnumerator MovePlayer(Vector3 direction)
    {

        isMoving = true;

        float elapsedTime = 0.1f;
        
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
