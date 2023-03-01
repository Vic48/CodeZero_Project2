using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    public bool flipXRotation = false;
    public bool flipYRotation = false;
    public float yLookMin = -80f; //lowest angle possible
    public float yLookMax = 80f;

    private float xRotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //  ---------   VERTICAL LOOK -------------
        //change xRotation based on mouse position
        xRotation += flipYRotation ? mouseY : -mouseY;

        //clamp rotation so it doesnt exceed 80 degrees
        xRotation = Mathf.Clamp(xRotation, yLookMin, yLookMax);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //  ---------   HORIZONTAL LOOK -------------
        playerBody.Rotate(Vector3.up, flipXRotation ? -mouseX : mouseX);
    }

    public bool GetShootHitPos(out RaycastHit hit)
    {
        //transform.position = starting position of raycast (where we are shooting from)
        //transform.TransformDirection = where we shoot towards & what direction
        //out hit = shoot until hit something

        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit);
    }
}
