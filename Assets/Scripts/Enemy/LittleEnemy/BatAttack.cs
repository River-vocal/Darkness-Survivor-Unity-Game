using System.Collections;
using System.Collections.Generic;
using MyEventArgs;
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    [SerializeField] private LittleEnemy littleEnemy;
	public int damage;
	private bool isDead;

	
	private void Awake()
	{
		damage = littleEnemy.damage;
	}

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getDeadFlag()
    {
    	return isDead;
    }


    private void OnTriggerStay2D(Collider2D col)
    {
    	if (col.gameObject.CompareTag("Player"))
    	{
            if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Enemy Attack 1")) {
                GetComponent<Animator>().SetTrigger("Attack");

                Energy energy = col.gameObject.GetComponent<Energy>();
                if  (energy.CurEnergy < damage) {
                    // GlobalAnalysis.player_status = "smallenemy_dead";
                    // Debug.Log("lose by: small enemy");
                }
                // GlobalAnalysis.smallenemy_damage += damage;
                col.GetComponent<Player>().TakeDamage(damage, new Object[]{gameObject});
            }
    		
    	}
    }

}
