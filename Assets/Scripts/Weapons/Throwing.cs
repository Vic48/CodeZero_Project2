using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Throwing : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject grenade;
    public WeaponSwitching swapWeapon;
    private LayerMask grenadeCollisionMask;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    //public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    public bool readyToThrow;
    public bool readyToAim;

    // Update is called once per frame
    void Update()
    {

        //check if left mouse button has been pressed while aiming
        if (Input.GetButton("Fire1") && readyToThrow && totalThrows > 0)
        {
            Throw();
        }

    }

    //  ----------  THROWING GRENADE    -------------
    public void Throw()
    {
        
        readyToThrow = false;
        //Debug.Log("FIRE IN THE HOLE");

        //instantiate objects to throw
        GameObject projectile = Instantiate(grenade, attackPoint.position, cam.rotation);

        //get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        //add force
        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        //implement cooldown
        Invoke(nameof(ResetThrow), throwCooldown);

    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
