using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    private float x;
    private float z;
    private Vector3 move;
    //Gravedad
    private Vector3 velocity;
    public float gravity = -15f;

    public Transform groundCheck;
    private float radius = 0.4f;
    public LayerMask mask;
    private bool isGrounded = false; // Servira para comprobar si el player esta en el suelo !

    public float jumpForce = 1f;
    // Update is called once per frame

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, radius, mask);
        
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = gravity;
        }
        
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;
        controller.Move(move*speed*Time.deltaTime);

        if (Input.GetButton("Jump") && isGrounded)
        {
            //Formula para hacer el salto mas realista 
            velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
