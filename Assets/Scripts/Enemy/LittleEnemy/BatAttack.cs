using System.Collections;
using System.Collections.Generic;
using MyEventArgs;
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    [SerializeField] private LittleEnemy littleEnemy;
	public int damage;
	private bool isAttacking;
	private bool isDead;

	
	private void Awake()
	{
		damage = littleEnemy.damage;
	}

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getAttackingFlag()
    {
    	return isAttacking;
    }

    public bool getDeadFlag()
    {
    	return isDead;
    }

    private void OnTrigeerEnter2D(Collider2D col)
    {
    	isAttacking = true;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
    	isAttacking = true;
    	if (col.gameObject.CompareTag("Player"))
    	{
    		GetComponent<Animator>().SetTrigger("Attack");

            Energy energy = col.gameObject.GetComponent<Energy>();
            if  (energy.CurEnergy < damage) {
                GlobalAnalysis.player_status = "smallenemy_dead";
                Debug.Log("lose by: small enemy");
            }
            GlobalAnalysis.smallenemy_damage += damage;
    		col.GetComponent<Player>().TakeDamage(damage, new Object[]{gameObject});
    	}
    }

    private void OnTriggerExit2D(Collider2D col)
    {
    	isAttacking = false;
    }

}
