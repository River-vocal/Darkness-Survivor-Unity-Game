using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPickup : MonoBehaviour
{
    [SerializeField] float floatingHieght = 1f;
    [SerializeField] float rotatingSpeed = 60f;
    private Vector3 initPosition;

    private void Start()
    {
        initPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotatingSpeed * Time.deltaTime, 0));
        transform.position = initPosition + new Vector3(0, floatingHieght * Mathf.Sin(Time.time), 0);
    }
}