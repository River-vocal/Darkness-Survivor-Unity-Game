using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float pauseTimeOnEnds = 1f;

    public Transform pos1;
    public Transform pos2;
    private Vector3 nextPos;
    private float pauseTimer;

    private void Awake()
    {
        nextPos = pos2.position;
        pauseTimer = pauseTimeOnEnds;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == pos1.position)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer > 0) return;
            nextPos = pos2.position;
            pauseTimer = pauseTimeOnEnds;
        }
        else if (transform.position == pos2.position)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer > 0) return;
            nextPos = pos1.position;
            pauseTimer = pauseTimeOnEnds;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}