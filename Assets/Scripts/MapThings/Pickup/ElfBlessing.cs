using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ElfBlessing : MonoBehaviour
{
    public float effectTime = 5f;
    public float rotationRadius = 1f;
    public float elevationOffset;
    public float rotationSpeed = 2f;
    public float elfSpeed = 10f;

    private Energy energy;
    private bool collided;

    private Transform target;
    private float angle;
    private GameObject icon;

    private void Awake()
    {
        icon = transform.GetChild(0).gameObject;
    }

    private Vector3 TargetOffset =>
        new Vector3(
            rotationRadius * Mathf.Cos(angle),
            elevationOffset,
            rotationRadius * Mathf.Sin(angle)
        );

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (collided) return;
        if (!col.gameObject.tag.Equals("Player")) return;
        target = col.transform;
        energy = col.gameObject.GetComponent<Energy>();
        energy.Damageable = false;
        collided = true;
        
        icon.SetActive(false);
        StartCoroutine("EndBlessing");
    }

    private void LateUpdate()
    {
        if (!collided || !target) return;
        angle += Time.deltaTime * rotationSpeed;

        transform.position = Vector3.MoveTowards(transform.position, target.position + TargetOffset,
            elfSpeed * Time.deltaTime);
    }

    private IEnumerator EndBlessing()
    {
        yield return new WaitForSeconds(effectTime);
        target = null;
        energy.Damageable = true;
        Destroy(gameObject);
    }
}