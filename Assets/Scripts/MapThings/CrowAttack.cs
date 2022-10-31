using System.Collections;
using System.Collections.Generic;
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
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, originY,
                    transform.position.z);
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
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, originY,
                    transform.position.z);
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
                GlobalAnalysis.player_status = "trap_dead";
                Debug.Log("lose by: trap");
            }
            GlobalAnalysis.trap_damage += damage;
            energy.CurEnergy -= damage;
        }
    }

    void beatBoss()
    {
        gameObject.SetActive(false);
    }
}
