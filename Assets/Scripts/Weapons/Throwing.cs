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

    [Header("Projectile Path")]
    [SerializeField]
    private LineRenderer _lineRenderer;
    [SerializeField]
    private Transform releasePos;
    [SerializeField]
    private int linePoints = 25;
    [SerializeField]
    [Range(0.01f, 0.25f)]
    private float timeBwtnPoints = 0.1f;
    [SerializeField]
    private Rigidbody prefabGrenadeRB; //prefab - grenade being thrown
    [SerializeField]
    private Camera _camera;

    public Mouse _mouse;
    public PlayerMovement playerMove;

    Vector3 mousePos;

    // Start is called before the first frame update
    void Awake()
    {
        int grenadeLayer = prefabGrenadeRB.gameObject.layer;

        for (int i = 0; i < 32; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(grenadeLayer, i))
            {
                grenadeCollisionMask |= 1 << i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check if aiming
        if (Input.GetButton("Fire2") && swapWeapon.selectedWeapon == 2)
        {
            //Debug.Log("THROW GRENADE");

            //lock camera but follow mouse position

            //  ---------   update  --------
            //_mouse.enabled = false;
            playerMove.enabled = false;

            mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

            //Debug.Log(mousePos.x);
            //Debug.Log(mousePos.x + ", " + mousePos.y);

            //draw line
            DrawProjection();

            //check if left mouse button has been pressed while aiming
            if (Input.GetButton("Fire1") && readyToThrow && totalThrows > 0)
            {
                Throw();
            }
        }
        else
        {
            _lineRenderer.enabled = false;

            //unlock camera
            //_mouse.enabled = true;
            playerMove.enabled = true;

            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    //  ----------  PROJECTION LINE    -------------
    private void DrawProjection()
    {
        Cursor.lockState = CursorLockMode.None;

        _lineRenderer.enabled = true;

        //allow rotation when aiming
        //throwForce = mousePos.y;

        //Debug.Log(throwForce + "," + mousePos.y);

        _lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBwtnPoints) + 1;
        Vector3 startPos = releasePos.position;
        Vector3 startVel = throwForce * (mousePos - cam.transform.forward) / prefabGrenadeRB.mass;

        //Vector3 startVel = throwForce * _camera.transform.forward / prefabGrenadeRB.mass;
        //Vector3 startVel = throwForce * (mousePos - _camera.transform.position).normalized / prefabGrenadeRB.mass;

        int i = 0;

        _lineRenderer.SetPosition(i, startPos);

        for (float time = 0; time < linePoints; time += timeBwtnPoints)
        {
            i++;

            Vector3 point = startPos + time * -startVel;
            point.y = startPos.y + startVel.y * time + (Physics.gravity.y / 0.3f * time * time);

            _lineRenderer.SetPosition(i, point);

            Vector3 lastPos = _lineRenderer.GetPosition(i - 1);

            if (Physics.Raycast(lastPos, 
                (point - lastPos).normalized, 
                out RaycastHit hit, 
                (point - lastPos).magnitude, grenadeCollisionMask))
            {
                _lineRenderer.SetPosition(i, hit.point);
                _lineRenderer.positionCount = i + 1;
                return;
            }
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
