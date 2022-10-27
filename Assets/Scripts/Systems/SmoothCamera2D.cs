using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera2D : MonoBehaviour
{
    [Header("Follow Target")]
    [SerializeField] public Transform Target;

    [Header("Smooth Camera Settings")]
    // Set x != 0 to offset the camera in front of the player
    [SerializeField] public Vector3 ForwardOffset = new Vector3(2, 1, -10);

    // Time limit to reach target position
    [SerializeField] public float SmoothTime = 0.15f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 flipedOffset;

    private Vector3 offset
    {
        get
        {
            if (Target.rotation.y > -0.5f)
            {
                // player face right
                return ForwardOffset;
            }
            else
            {
                // player face left
                return flipedOffset;
            }
        }
    }

    private void Awake()
    {
        flipedOffset = new Vector3(-ForwardOffset.x, ForwardOffset.y, ForwardOffset.z);
    }


    // Update is called once per frame
    private void Update()
    {
        if (Target)
        {
            Vector3 targetPosition = Target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
        }
    }
}
