using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTentacle : MonoBehaviour
{
    [SerializeField] private LittleEnemy littleEnemy;
    private int damage;

    private void Awake()
	{
		damage = littleEnemy.damage;
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
    	if (col.gameObject.CompareTag("Player"))
    	{
            Energy energy = col.gameObject.GetComponent<Energy>();
            if  (energy.CurEnergy < damage) {
                GlobalAnalysis.player_status = "smallenemy_dead";
                Debug.Log("lose by: small enemy");
            }
            GlobalAnalysis.smallenemy_damage += damage;
    		col.GetComponent<Player>().TakeDamage(damage, new Object[]{gameObject});
    	}
    }
}
