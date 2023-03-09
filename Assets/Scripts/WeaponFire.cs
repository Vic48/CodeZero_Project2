using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public float cooldown = 0.5f;

    private bool isFiring = false;

    [SerializeField] private GameObject _bulletHolePrefab; //Bullet hole
    [SerializeField] private GameObject _scrumpPrefab; //Scrump

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Fire1 = left ctrl or left mouse button
        if (Input.GetButtonDown("Fire1") & !isFiring)
        {
            //shoot
            StartCoroutine(FireWeapon());
        }
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
                float x = Random.Range(0,2);
                
                if (x == 0)
                {
                    //returns true if ray touches something
                    GameObject obj = Instantiate(_bulletHolePrefab, hitInfo.point + (hitInfo.normal * 0.01f), Quaternion.identity);
                    //Instantiating the bullet hole object
                    obj.transform.LookAt(hitInfo.point, hitInfo.normal);

                    //can show bullet hole image if hit wall/ground etc - can fade away after awhile
                    Destroy(obj, 1);
                }

                else if (x == 1)
                {
                    //returns true if ray touches something
                    GameObject obj = Instantiate(_scrumpPrefab, hitInfo.point + (hitInfo.normal * 0.01f), Quaternion.identity);
                    //Instantiating the bullet hole object
                    obj.transform.LookAt(hitInfo.point, hitInfo.normal);

                    //can show bullet hole image if hit wall/ground etc - can fade away after awhile
                    Destroy(obj, 1);
                    Debug.Log("scrump");
                }

            }

            //check if item can be hit
            if (hit.collider.GetComponent<TargetScript>() != null)
            {
                //if target can be hit, destroy
                hit.collider.GetComponent<TargetScript>().DoHit();
            }
        }

        //play particle animation
        this.GetComponentInChildren<ParticleSystem>().Play();

        //wait for cooldown
        yield return new WaitForSeconds(cooldown);

        isFiring = false;
    }
}
