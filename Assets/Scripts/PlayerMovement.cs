using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public static int health = 100;
    public bool isDead = false;
    public TMP_Text playerHPText;

    public CharacterController controller;
    public float speed;

    public float gravity = -18f;
    public float jumpHeight = 0.03f;

    public float normalHeight, crouchHeight;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    public float groundStop = -0.1f;

    public Vector3 velocity;
    public bool isGrounded = false; //false = on the ground

    // Update is called once per frame
    void Update()
    {
        playerHPText.text = "Player HP: " + health.ToString();
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

        //  ---------   SPRINT -------------
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            speed = 12f;
            //Debug.Log("SHIFT DOWN, ON GROUND" + speed);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded)
        {
            speed = 6f;
            //Debug.Log("SHIFT UP, ON GROUND" + speed);
        }

        //  ---------   CROUCH -------------
        if (Input.GetKeyDown(KeyCode.C))
        {
            controller.height = crouchHeight;

            speed = 2f;
            //Debug.Log("CROUCH");
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            controller.height = normalHeight;

            speed = 6f;
            //Debug.Log("STAND");
        }
        

        controller.Move(velocity);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health < 0)
        {
            isDead = true;
        }
    }
}
