using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float strength = 5f;
    [SerializeField] private float duration = 0.3f;
    public UnityEvent onBegin, onEnd;

    public void Trigger(GameObject sender)
    {
        StopAllCoroutines();
        onBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rigidbody2D.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(duration);
        rigidbody2D.velocity = Vector2.zero;
        onEnd?.Invoke();
    }
}
