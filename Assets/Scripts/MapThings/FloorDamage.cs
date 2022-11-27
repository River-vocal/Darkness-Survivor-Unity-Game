using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorDamage : MonoBehaviour
{
    private float damageMultiplier;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("DarkLevel1") || 
            SceneManager.GetActiveScene().name.Equals("Level4"))
        {
            damageMultiplier = 0.4f;
        }
        else
        {
            damageMultiplier = 0.2f;
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
            float maxEner = collision.gameObject.GetComponent<Energy>().MaxEnergy;
            collision.gameObject.GetComponent<Player>().TakeDamage(damageMultiplier * maxEner);
        }
    }
}
