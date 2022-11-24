using System;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject respawnEffect;
    public Vector3 respawnLocation;
    public int deathTooManyThreshold = 10;

    private int numOfDeath;
    private float timer;

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
        respawnEffect.SetActive(true);
        timer = 2f;
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

    private void Update()
    {
        if (timer <= 0) return;
        timer -= Time.deltaTime;
        if (timer <= 0) respawnEffect.SetActive(false);
    }
}