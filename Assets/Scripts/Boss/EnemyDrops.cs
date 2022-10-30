using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] public int damage;
    [SerializeField] public string color;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    private Animator anim;
    
    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y,
                    transform.position.z);
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y,
                    transform.position.z);
            }
            else
            {
                movingLeft = true;
            }
        }
    }

    public void dropDeath()
    {
        speed = 0;
        anim.SetTrigger("dropDeath");
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            switch (color) {
             
                case "red":    
                    // if  (col.GetComponent<Energy>().CurEnergy < damage) {
                    //     GlobalAnalysis.player_status = "trap_dead";
                    //     Debug.Log("lose by: trap");
                    // }
                    // GlobalAnalysis.trap_damage += damage;
                    col.GetComponent<Energy>().CurEnergy -= damage;
                    break;
 
                case "green":
                    // GlobalAnalysis.healing_energy += damage;
                    col.GetComponent<Energy>().CurEnergy += damage;
                    break;

                default:
                    break;
            }
            
        }
    }
    
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
