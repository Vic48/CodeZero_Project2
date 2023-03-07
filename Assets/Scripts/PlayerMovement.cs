using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed;

    public float gravity = -18f;
    public float jumpHeight = 0.03f;

    //public float crouchHeight;
    public GameObject Cam;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    public float groundStop = -0.1f;

    private Vector3 velocity;
    private bool isGrounded = false; //false = on the ground
    private bool jump = false; //nvr jump
    private bool isCrouch = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(speed);
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
                jump = true;
            }
        }

        //  ---------   SPRINT -------------
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && jump)
        {
            speed = 12f;
            Debug.Log("SHIFT DOWN, ON GROUND, NVR JUMP" + speed);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded && jump)
        {
            speed = 6f;
            Debug.Log("SHIFT UP, ON GROUND, NVR JUMP" + speed);
        }

        //  ---------   CROUCH ------------- ...?
        if (!isCrouch)
        {
            if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            {
                //not crouching but debug and speed works
                //crouchHeight = this.transform.position.y - 0.5f;
                //transform.position = new Vector3(0, 0.5f, 0);
                //velocity = new Vector3(0, 0.5f, 0);
                Cam.transform.position = new Vector3(Cam.transform.position.x, Cam.transform.position.y - 0.2f, Cam.transform.position.z); 
                speed = 2f;
                Debug.Log("CROUCH");
                isCrouch = true;
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.C) && isGrounded)
            {
                //crouchHeight = this.transform.position.y;
                //transform.position = new Vector3(0, 1f, 0);
                //velocity = new Vector3(0, 1f, 0);
                Cam.transform.position = new Vector3(Cam.transform.position.x, Cam.transform.position.y + 0.2f, Cam.transform.position.z);
                speed = 6f;
                Debug.Log("STAND");
                isCrouch = false;
            }
        }
        
        

        controller.Move(velocity);
    }
}
