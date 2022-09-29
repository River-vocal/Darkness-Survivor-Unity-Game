using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerJump : MonoBehaviour
{
    private CharacterController cc;
    private Vector3 velocity;
    private bool onGround;
    [SerializeField]private float jumpHeight = 5.0f;
    private bool jumpPressed = false;
    private float gravity = -9.81f;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnJump() {
        Debug.Log("jump pressed!");
    }
}
