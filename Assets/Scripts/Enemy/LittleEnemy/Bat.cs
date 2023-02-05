using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
	[SerializeField] private LittleEnemy littleEnemy;
	
	[SerializeField] public Transform pos1;
	[SerializeField] public Transform pos2;
	[SerializeField] public Transform pos3;
	[SerializeField] public Transform pos4;
	[SerializeField] public GameObject bat;
	
	public float moveSpeed;
	private Vector3 nextPos;
	private bool movingLeft;
	private bool inAttackArea = false;
	private Vector3 playerPos;
    public VisualEffectSystemManager VisualEffectSystemManager;

    // Start is called before the first frame update

    private void Awake()
    {
	    moveSpeed = littleEnemy.speed;
    }

    void Start()
    {
    	if (bat.transform.position.x < pos1.position.x) {
    		nextPos = pos1.position;
    	} else if (bat.transform.position.x < pos2.position.x) {
    		nextPos = pos2.position;
    	} else if (bat.transform.position.x < pos3.position.x) {
    		nextPos = pos3.position;
    	} else {
    		nextPos = pos4.position;
    	}
    }

    // Update is called once per frame
    void Update()
    {
		if (littleEnemy.GetBeAttackedStatus()) {
			bat.GetComponent<Animator>().SetTrigger("Death");
			littleEnemy.SetBeAttackedStatus(false);
			littleEnemy.SetDeathStatus(true);
			bat.GetComponent<Collider2D>().enabled = false;
			Invoke("deactivate", 0.8f);
		}
    	// if (bat.GetComponent<BatAttack>().getDeadFlag()) {
    	// 	deactivate();
    	// }

    	if (!inAttackArea) {
    		if (bat.transform.position == pos1.position) {
	        	nextPos = pos2.position;
	        	movingLeft = false;
	        	flip();
	        } else if (bat.transform.position == pos2.position) {
	        	if (movingLeft) {
	        		nextPos = pos1.position;
	    		} else {
	    			nextPos = pos3.position;
	    		}
	        } else if (bat.transform.position == pos3.position) {
	        	if (movingLeft) {
	        		nextPos = pos2.position;
	        	} else {
	        		nextPos = pos4.position;
	        	}
	        } else if (bat.transform.position == pos4.position) {
	        	nextPos = pos3.position;
	        	movingLeft = true;
	        	flip();
	        }
	        bat.transform.position = Vector3.MoveTowards(bat.transform.position, nextPos, moveSpeed * Time.deltaTime);
    	}
        // in attack area
    	else {
    		if (playerPos.x < bat.transform.position.x && !movingLeft) {
    			movingLeft = true;
    			flip();
    		} else if (playerPos.x > bat.transform.position.x && movingLeft) {
    			movingLeft = false;
    			flip();
    		}

			Vector3 newPos = new Vector3(playerPos.x, playerPos.y + 2.2f, bat.transform.position.z);
			bat.transform.position = Vector3.MoveTowards(bat.transform.position, newPos, 2 * moveSpeed * Time.deltaTime);    		
    	}
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
		if (col.gameObject.CompareTag("Player")) {
			inAttackArea = true;
		}
    }

    private void OnTriggerStay2D(Collider2D col)
    {
    	if (col.gameObject.CompareTag("Player")) {
    		playerPos = col.gameObject.transform.position;
    		if (playerPos.x <= pos2.position.x) {
    			nextPos = movingLeft ? pos1.position : pos2.position;
    		} else if (playerPos.x >= pos3.position.x) {
    			nextPos = movingLeft ? pos3.position : pos4.position;
    		} else {
    			nextPos = movingLeft ? pos2.position : pos3.position;
    		}
    	}
    }

    private void OnTriggerExit2D(Collider2D col)
 	{
		if (col.gameObject.CompareTag("Player")) {
			inAttackArea = false;
		}
 	}

    private void flip()
    {
    	bat.transform.Rotate(0, 180, 0);
    }

    private void deactivate()
    {
        VisualEffectSystemManager.GenerateEvilPurpleExplode(bat.transform);
    	gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
        Gizmos.DrawLine(pos2.position, pos3.position);
        Gizmos.DrawLine(pos3.position, pos4.position);
    }
}
