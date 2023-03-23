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
    //public KeyCode throwKey = KeyCode.Mouse0;
    //public KeyCode aimKey = KeyCode.Mouse1;
    public float throwForce;
    public float throwUpwardForce;

    public bool readyToThrow;
    public bool readyToAim;

    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    private Transform releasePos;

    [Header("Display Controls")]
    [SerializeField]
    private int linePoints = 25;
    [SerializeField]
    [Range(0.01f, 0.25f)]
    private float timeBwtnPoints = 0.1f;
    [SerializeField]
    private Rigidbody prefabGrenadeRB;
    [SerializeField]
    private Camera _camera;

    public Transform camPos;
    private float yRotation = 0;

    public Mouse _mouse;

    public WeaponSwitching swapWeapon;
    private LayerMask grenadeCollisionMask;

    // Start is called before the first frame update
    void Awake()
    {
        //readyToThrow = true;

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
            Debug.Log("THROW GRENADE");

            //lock camera but follow mouse position

            //  ---------   update  --------
            //game scene camera locks but scene cam moves
            //no line renderer tho
            //_camera.enabled = false;
            _lineRenderer.enabled = true;

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
            //_camera.enabled = true;
        }

    }

    //  ----------  PROJECTION LINE    -------------
    private void DrawProjection()
    {
        //_lineRenderer.enabled = true;
        _lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBwtnPoints) + 1;
        Vector3 startPos = releasePos.position;
        Vector3 startVel = throwForce * _mouse.transform.forward / prefabGrenadeRB.mass;
        
        int i = 0;
        _lineRenderer.SetPosition(i, startPos);

        for (float time = 0; time < linePoints; time += timeBwtnPoints)
        {
            i++;

            Vector3 point = startPos + time * startVel;
            point.y = startPos.y + startVel.y * time + (Physics.gravity.y / 2f * time * time);

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
