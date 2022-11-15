using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorDamage : MonoBehaviour
{
    private float damageValue;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("DarkLevel1") || 
            SceneManager.GetActiveScene().name.Equals("Level4"))
        {
            damageValue = 30f;
        }
        else
        {
            damageValue = 10f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // if  (collision.gameObject.GetComponent<Energy>().CurEnergy < damageValue) {
            //     GlobalAnalysis.player_status = "trap_dead";
            //     Debug.Log("lose by: trap");
            // }

            
            // GlobalAnalysis.trap_damage += damageValue;
            collision.gameObject.GetComponent<Energy>().CurEnergy -= damageValue;
            collision.gameObject.GetComponent<Player>().TakeDamage();
        }
    }
}
