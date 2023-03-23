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

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public KeyCode aimKey = KeyCode.Mouse1;
    public float throwForce;
    public float throwUpwardForce;

    public bool readyToThrow;
    public bool readyToAim;

    //checking mouse pos
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;

    //public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //readyToThrow = true;
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(aimKey))
        {
            Debug.Log("THROW GRENADE");
            //Throw();
        }
        else if (Input.GetKeyUp(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }

        //if (readyToAim == false)
        //{
        //    if (Input.GetKeyDown(aimKey))
        //    {
        //        mousePressDownPos = Input.mousePosition;
        //        readyToAim = true;
        //    }

        //    Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
        //    Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y)) * throwForce;

        //    if (readyToAim == true)
        //    {
        //        Debug.Log("DRAW LINE");
        //        DrawProjection.Instance.UpdateTrajectory(forceV, rb, transform.position);
        //    }

        //    if (Input.GetKeyUp(aimKey))
        //    {
        //        Debug.Log("HIDE LINE");
        //        DrawProjection.Instance.HideLine();
        //        mouseReleasePos = Input.mousePosition;

        //        readyToAim = false;
        //    }
        //}

        //if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0 && readyToAim == true)
        //{

        //    //Throw(mouseReleasePos - mousePressDownPos);
        //    Throw();
        //}



    }

    //  ----------  CHECKING MOUSE POS    -------------
    //private void OnMouseDown()
    //{
    //    mousePressDownPos = Input.mousePosition;
    //    readyToThrow = true;
    //}

    //private void OnMouseDrag()
    //{
    //    Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
    //    Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y)) * throwForce;

    //    if (readyToThrow == true)
    //    {
    //        Debug.Log("DRAW LINE");
    //        DrawProjection.Instance.UpdateTrajectory(forceV, rb, transform.position);
    //    }
    //}

    //private void OnMouseUp()
    //{
    //    Debug.Log("HIDE LINE");
    //    DrawProjection.Instance.HideLine();
    //    mouseReleasePos = Input.mousePosition;
    //    Throw(mouseReleasePos - mousePressDownPos);
    //}

    //[SerializeField]
    //private float forceMultiplier = 2;


    //  ----------  THROWING GRENADE    -------------
    //public void Throw(Vector3 force)
    //{
    //    if (readyToThrow == false && readyToAim == true)
    //    {
    //        //readyToThrow = false;
    //        Debug.Log("FIRE IN THE HOLE");

    //        //instantiate objects to throw
    //        GameObject projectile = Instantiate(grenade, attackPoint.position, cam.rotation);

    //        //get rigidbody component
    //        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

    //        //add force
    //        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;

    //        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

    //        totalThrows--;

    //        //implement cooldown
    //        Invoke(nameof(ResetThrow), throwCooldown);

    //        return;
    //    }
        
    //}

    public void Throw()
    {
        
        readyToThrow = false;
        Debug.Log("FIRE IN THE HOLE");

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
