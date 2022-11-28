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
    public float blinkTime = 5f;

    private Energy energy;
    private bool collided;

    private Transform target;
    private float angle;
    private GameObject icon;
    public Renderer renderer;
    private float blinkInterval = 0.1f;
    private bool hinted = false;
    
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
        TopHintArea.Hint("invulnerable", 3);
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
        float timeElapsed = 0;
        
        while (timeElapsed < effectTime)
        {
            if (timeElapsed > effectTime - blinkTime)
            {
                if (!hinted)
                {
                    TopHintArea.Hint("Elf Bless would disappear", 2);
                    hinted = true;
                }
                if (renderer.enabled)
                {
                    renderer.enabled = false;
                }
                else
                {
                    renderer.enabled = true;
                }
            }
            timeElapsed += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }
        target = null;
        energy.Damageable = true;
        Destroy(gameObject);
    }
}