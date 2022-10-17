using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    [SerializeField] private Boss boss;
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

    public void ActivateProjectile(int attackDamage, bool isFaceLeft)
    {
        damage = attackDamage;
        hit = false;
        Activate(isFaceLeft);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.name == "Ground")
        {
            hit = true;
            anim.SetTrigger("explode");
            coll.enabled = false;
        }

        else if (collision.tag == "Player")
        {
            GlobalAnalysis.boss_damage += damage;
            if  (collision.GetComponent<Energy>().CurEnergy < damage) {
                GlobalAnalysis.player_status = "boss_attack_dead";
                Debug.Log("lose by: boss");
            }
            hit = true;
            collision.GetComponent<Energy>().CurEnergy -= damage;
            anim.SetTrigger("explode");
            coll.enabled = false;
        }
        
        
    
        /*if (anim != null && collision.name != "Fire")
            //anim.SetTrigger("explode"); //When the object is a fireball explode it
            gameObject.SetActive(false);
        else
        {
            
            gameObject.SetActive(false);
        } //When this hits any object deactivate arrow*/
    }
    private void Deactivate()
    {
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    private void Activate(bool isFaceLeft)
    {
        if (!isFaceLeft)
        {
            gameObject.transform.Rotate(0, 180, 0);
        }
        
        gameObject.SetActive(true);
    }
}