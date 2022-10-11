using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private Collider2D coll;
    private int damage;
    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    public void ActivateProjectile(int attackDamage)
    {
        damage = attackDamage;
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().CurHealth -= damage;
        }
            
        coll.enabled = false;
    
        if (anim != null && collision.name != "Fire")
            //anim.SetTrigger("explode"); //When the object is a fireball explode it
            gameObject.SetActive(false);
        else
        {
            
            gameObject.SetActive(false);
        } //When this hits any object deactivate arrow
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

