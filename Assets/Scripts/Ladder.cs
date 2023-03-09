using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class Ladder : MonoBehaviour
{
    public Transform controller;
    bool insideLadder = false;
    public float speed = 3.3f;
    public PlayerMovement playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerMovement>();
        insideLadder = false;
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            playerInput.enabled = false;
            insideLadder = true;
            //Debug.Log("TOUCHING LADDER");

            if (insideLadder == true && Input.GetKey(KeyCode.W))
            {
                controller.transform.position += Vector3.up / speed;
                //Debug.Log("GOING UP");
            }
            if (insideLadder == true && Input.GetKey(KeyCode.S))
            {
                controller.transform.position += Vector3.down / speed;
               // Debug.Log("GOING DOWN");

            }
        }

        if (insideLadder == true && col.gameObject.tag == "LadderTop")
        {
            if(insideLadder == true && Input.GetKey(KeyCode.W))
            {
                controller.transform.position += - Vector3.right / speed;
                //Debug.Log("GOING AJSVDAGJSAVH");
            }
            //Debug.Log("dfdfdfdfdfd LADDER");

        }

        if (insideLadder == true && col.gameObject.tag == "Bottom")
        {
            if (insideLadder == true && Input.GetKey(KeyCode.S))
            {
                controller.transform.position += -Vector3.left / speed;
                playerInput.enabled = true;

                //Debug.Log("GOING AJSVDAGJSAVH");
            }
            Debug.Log("EXITTTTT");
        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "LadderTop")
        {
            playerInput.enabled = true;
            insideLadder = false;
            Debug.Log("AWAY");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
