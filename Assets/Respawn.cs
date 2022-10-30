using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Vector3 respawnLocation;
    // Start is called before the first frame update
    void Start()
    {   
        respawnLocation = transform.transform.position;
    }

    public void Remake()
    {
        resetPlayer();
        gameObject.transform.position = respawnLocation;
    }

    private void resetPlayer()
    {
        Energy energy = gameObject.GetComponent<Energy>();
        energy.CurEnergy = energy.MaxEnergy;
        
        // reset more variables here
    }
    
}
