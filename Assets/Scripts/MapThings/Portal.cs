using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] public Transform target;
    private bool canTeleport;
    private GameObject player;
    private Canvas hintCanvas;

    private bool teleport => player.GetComponent<PlayerControllerNew>().usePressed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hintCanvas = gameObject.GetComponentInChildren<Canvas>();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canTeleport = true;
            hintCanvas.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canTeleport = false;
        hintCanvas.enabled = false;
    }

    private void Update()
    {
        if (canTeleport && teleport)
        {
            canTeleport = false;
            Invoke("teleportPlayer", 0.5f);
        }
    }

    void teleportPlayer()
    {
        if (target)
            player.transform.position = target.position;
    }
}
