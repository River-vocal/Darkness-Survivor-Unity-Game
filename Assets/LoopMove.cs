using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float pauseTimeOnEnds = 0f;

    public Transform[] pos;
    private int nextPosIndex;
    private float pauseTimer;

    private void Awake()
    {
        nextPosIndex = 0;
        pauseTimer = pauseTimeOnEnds;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == pos[nextPosIndex].position)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer > 0) return;
            nextPosIndex = (nextPosIndex + 1) % pos.Length;
            pauseTimer = pauseTimeOnEnds;
        }

        transform.position =
            Vector3.MoveTowards(transform.position, pos[nextPosIndex].position, moveSpeed * Time.deltaTime);
    }
}