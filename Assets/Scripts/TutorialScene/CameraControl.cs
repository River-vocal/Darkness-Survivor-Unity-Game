using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float offsetX = 4f;

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 desiredPosition = new Vector3(target.position.x + offsetX, 0, -10);
        Vector3 smoothedPostion = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPostion;
    }
}
