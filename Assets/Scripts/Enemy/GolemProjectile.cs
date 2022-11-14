using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemProjectile : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    Rigidbody2D rb;
    Player target;
    private Transform myTransform;
    Vector2 moveDirection;
    

    void Awake()
    {
        rb.GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Player>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("Hit projectile!!!!!!!!!!!!!!!!!!!!!!");
    //         Destroy(gameObject);
    //     }
        
    // }
}
