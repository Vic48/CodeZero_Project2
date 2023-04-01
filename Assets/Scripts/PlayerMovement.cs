using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{
    public int health;
    public bool isDead = false;

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

    public TextAsset dialogueTextJSON;
    public DialogueLine dialogueLine;

    private TextFromJson text;

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

    private void OnTriggerStay (Collider collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            StartCoroutine(NpcDialogueSequence());
        }
    }
    private IEnumerator NpcDialogueSequence()
    {
        // Search for objects where the "Character" field is "Start"
        var filteredList = text.DialogueText.Where(obj => obj.character == "NPC");

        // Sort the filtered list based on the "Order" field
        var sortedList = filteredList.OrderBy(obj => obj.order);

        foreach (DialogueText dialogueText in sortedList)
        {
            Debug.Log(dialogueText);
            dialogueLine.transform.gameObject.SetActive(false);
            dialogueLine.finished = false;
            dialogueLine.transform.gameObject.SetActive(true);
            dialogueLine.setInput(dialogueText.text);
            yield return new WaitUntil(() => dialogueLine.finished);
        }
        gameObject.SetActive(false);
    }
}
