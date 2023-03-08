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
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Ladder")
        {
            playerInput.enabled = false;
            insideLadder = true;
            Debug.Log("TOUCHING LADDER");
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            playerInput.enabled = true;
            insideLadder = false;
            Debug.Log("AWAY");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (insideLadder == true && Input.GetKeyDown(KeyCode.W))
        {
            controller.transform.position += Vector3.up / speed;
            Debug.Log("GOING UP");
        }
        if (insideLadder == true && Input.GetKeyDown(KeyCode.S))
        {
            controller.transform.position += Vector3.down / speed;
            Debug.Log("GOING DOWN");
        }
    }
}
