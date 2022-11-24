using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Respawn : MonoBehaviour
{
    public Vector3 respawnLocation;
    public int deathTooManyThreshold = 10;

    private int numOfDeath;

    // Start is called before the first frame update
    void Start()
    {
        respawnLocation = transform.transform.position;
        numOfDeath = 0;
    }

    public void Remake()
    {
        resetPlayer();
        gameObject.transform.position = respawnLocation;
        numOfDeath++;
        if (numOfDeath >= deathTooManyThreshold)
        {
            Instantiate(GameAssets.i.pfElfBlessing, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        }
    }

    private void resetPlayer()
    {
        Energy energy = gameObject.GetComponent<Energy>();
        Player player = GetComponent<Player>();
        player.StateMachine.ChangeState(player.IdleState);
        energy.CurEnergy = energy.MaxEnergy;

        // reset more variables here
    }
}