using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 4f;

    public float gravity = -18f;
    public float jumpHeight = 0.03f;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    public float groundStop = -0.1f;

    private Vector3 velocity;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //  ---------   MOVEMENT -------------
        controller.Move(Vector3.ClampMagnitude(move, 1f) * speed * Time.deltaTime);

        //  ---------   GRAVITY -------------
        velocity.y += gravity * Time.deltaTime * Time.deltaTime;

        //  ---------   GROUND CHECK -------------
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = groundStop;
        }

        //  ---------   JUMP -------------
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                velocity.y = jumpHeight; //cancel 
            }
        }

        controller.Move(velocity);
    }
}
