using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class Ladder : MonoBehaviour
{
    public Transform playerMovement;
    bool insideLadder;
    public float ladderHeight = 3.3f;
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
            insideLadder = !insideLadder;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            playerInput.enabled = true;
            insideLadder = !insideLadder;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (insideLadder && Input.GetKey("w"))
        {
            playerMovement.transform.position += Vector3.up / ladderHeight;
        }
    }
}
