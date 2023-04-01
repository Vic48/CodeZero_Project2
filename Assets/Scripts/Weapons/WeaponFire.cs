using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public float cooldown = 0.5f;

    public bool isFiring = false;

    public int damage = 5;

    public WeaponSwitching switchWeapon;

    [Header("Rifle")]
    public Animator animator;
    private bool isScoped = false;
    public GameObject scopeOverlay;

    [Header("Camera Stuff")]
    public GameObject weaponCamera;
    public Camera mainCam;
    public float scopedFOV = 15f;
    private float normalFOV;

    //public Throwing throwScript;
    //public Mouse _mouse;

    //Vector3 mousePos;

    [SerializeField] private GameObject _bulletHolePrefab; //Bullet hole
    [SerializeField] private GameObject _scrumpPrefab; //Scrump
    [SerializeField] private GameObject _xiaoPrefab; //Xiao
    [SerializeField] private GameObject _buttPrefab; //Butt

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check if its rifle
        if (switchWeapon.selectedWeapon == 1)
        {
            //Fire1 = left ctrl or left mouse button
            if (Input.GetButtonDown("Fire1") & !isFiring && isScoped == true)
            {
                //shoot
                StartCoroutine(FireWeapon());
                //Debug.Log("FIRING");
                gameObject.GetComponent<AudioSource>().Play();
            }

            //Fire 2 = left alt or right mouse button
            if (Input.GetButtonDown("Fire2"))
            {
                //Debug.Log("SCOPE RIFLE");
                isScoped = !isScoped;
                animator.SetBool("Scoped", isScoped);

                if (isScoped)
                {
                    StartCoroutine(OnScope());
                }
                else
                {
                    UnScope();
                }
            }
        }
        else if (switchWeapon.selectedWeapon == 0)
        {
            //Fire1 = left ctrl or left mouse button
            if (Input.GetButtonDown("Fire1") & !isFiring)
            {
                //shoot
                StartCoroutine(FireWeapon());
                //Debug.Log("FIRING");
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
        //else if (switchWeapon.selectedWeapon == 2)
        //{
        //    if (Input.GetButton("Fire2"))
        //    {
        //        throwScript.DrawProjection();
        //        _mouse.enabled = false;

        //        //check if left mouse button has been pressed while aiming
        //        if (Input.GetButton("Fire1") && throwScript.readyToThrow && throwScript.totalThrows > 0)
        //        {
        //            StartCoroutine(FireGrenade());
        //        }
        //    }
        //    else
        //    {
        //        throwScript._lineRenderer.enabled = false;
        //        _mouse.enabled = true;
        //    }
        //}
    }

    //coroutine
    private IEnumerator FireWeapon()
    {
        isFiring = true;

        //shooting stuff
        RaycastHit hit;
        if (this.GetComponentInParent<Mouse>().GetShootHitPos(out hit))
        {
            //returns true if we click the mouse button
            RaycastHit hitInfo; //Contains raycast hit infomation
            if (Physics.Raycast(origin: transform.position, direction: transform.forward, out hitInfo))
            {
                float x = Random.Range(0,4);
                
                if (x == 0)
                {
                    //returns true if ray touches something
                    GameObject obj = Instantiate(_bulletHolePrefab, hitInfo.point + (hitInfo.normal * 0.01f), Quaternion.identity);
                    //Instantiating the bullet hole object
                    obj.transform.LookAt(hitInfo.point, hitInfo.normal);

                    //can show bullet hole image if hit wall/ground etc - can fade away after awhile
                    Destroy(obj, 1);
                }

                if (x == 1)
                {
                    //returns true if ray touches something
                    GameObject obj = Instantiate(_scrumpPrefab, hitInfo.point + (hitInfo.normal * 0.01f), Quaternion.identity);
                    //Instantiating the bullet hole object
                    obj.transform.LookAt(hitInfo.point, hitInfo.normal);

                    //can show bullet hole image if hit wall/ground etc - can fade away after awhile
                    Destroy(obj, 1);
                }

                if (x == 2)
                {
                    //returns true if ray touches something
                    GameObject obj = Instantiate(_xiaoPrefab, hitInfo.point + (hitInfo.normal * 0.01f), Quaternion.identity);
                    //Instantiating the bullet hole object
                    obj.transform.LookAt(hitInfo.point, hitInfo.normal);

                    //can show bullet hole image if hit wall/ground etc - can fade away after awhile
                    Destroy(obj, 1);
                }

                if (x == 3)
                {
                    //returns true if ray touches something
                    GameObject obj = Instantiate(_buttPrefab, hitInfo.point + (hitInfo.normal * 0.01f), Quaternion.identity);
                    //Instantiating the bullet hole object
                    obj.transform.LookAt(hitInfo.point, hitInfo.normal);

                    //can show bullet hole image if hit wall/ground etc - can fade away after awhile
                    Destroy(obj, 1);
                }

            }

            //check if item can be hit
            if (hit.collider.GetComponent<EnemyAI>() != null)
            {
                //if target can be hit, attack
                hit.collider.GetComponent<EnemyAI>().TakeDamage(damage);
            }
        }

        //play particle animation
        this.GetComponentInChildren<ParticleSystem>().Play();

        //wait for cooldown
        yield return new WaitForSeconds(cooldown);

        isFiring = false;
    }

    IEnumerator OnScope()
    {
        yield return new WaitForSeconds(0.15f);

        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);

        normalFOV = mainCam.fieldOfView;
        mainCam.fieldOfView = scopedFOV;
    }

    public void UnScope()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);

        mainCam.fieldOfView = normalFOV;
    }
}
