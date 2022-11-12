using System.Collections;
using System.Collections.Generic;
using MyEventArgs;
using UnityEngine;

public class CrowAttack : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] public int damage;
	[SerializeField] private float leftBoundary;
	[SerializeField] private float rightBoundary;
	private bool movingLeft;
	private float originY;
    private Health health;
    private float countDown = 0;

    private void Awake() {
        health = GetComponent<Health>();
        health.OnDamaged += health_OnDamaged;
        health.OnDead += health_OnDead;
    }

    private void health_OnDamaged(object sender, System.EventArgs e)
    {
        int damage = ((IntegerEventArg) e).Value;

    }
    private void health_OnDead(object sender, System.EventArgs e)
    {
        //Analysis Data
        // GlobalAnalysis.is_boss_killed = true;
        Invoke("beatBoss", 0.5f);
    }


    // Start is called before the first frame update
    void Start()
    {
        originY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
		if (movingLeft)
        {
            if (transform.position.x > leftBoundary)
            {
                if (countDown > 0) {
                    countDown -= Time.deltaTime;
                    float newY;
                    if (countDown > 0.5) {
                        newY = transform.position.y - 5 * speed * Time.deltaTime;
                        if (newY < originY - 2) {
                            newY = originY - 2;
                        }
                    }
                    else {
                        newY = transform.position.y + 8 * speed * Time.deltaTime;
                        if (newY > originY) {
                            newY = originY;
                        }
                    }
                    transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, newY, transform.position.z);
                }
                else {
                    transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, originY, transform.position.z);
                }
            }
            else
            {
                movingLeft = false;
                flip();
            }
        }
        else
        {
            if (transform.position.x < rightBoundary)
            {
                if (countDown > 0) {
                    countDown -= Time.deltaTime;
                    float newY;
                    if (countDown > 0.5) {
                        newY = transform.position.y - 5 * Time.deltaTime;
                        if (newY < originY - 2) {
                            newY = originY - 2;
                        }
                    }
                    else {
                        newY = transform.position.y + 8 * Time.deltaTime;
                        if (newY > originY) {
                            newY = originY;
                        }
                    }
                    transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, newY, transform.position.z);
                }
                else {
                    transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, originY, transform.position.z);
                }     
            }
            else
            {
                movingLeft = true;
                flip();
            }
        }
    }

    void flip()
    {
    	transform.Rotate(0, 180, 0);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
        	// isTrigger = true;
            countDown = 1.5f;
        	GetComponent<Animator>().Play("attack");

        	GameObject player = col.gameObject;
        	if (player.transform.position.x <= transform.position.x) {
	    		if (!movingLeft) {
	    			movingLeft = true;
	    			flip();
	    		}
	    	}
	    	else {
	    		if (movingLeft) {
	    			movingLeft = false;
	    			flip();
	    		}
	    	}

            Energy energy = col.gameObject.GetComponent<Energy>();
            if  (energy.CurEnergy < damage) {
                GlobalAnalysis.player_status = "smallenemy_dead";
                Debug.Log("lose by: small enemy");
            }
            GlobalAnalysis.smallenemy_damage += damage;
            energy.CurEnergy -= damage;
        }
    }

    void beatBoss()
    {
        gameObject.SetActive(false);
    }
}
