using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public float cooldown = 0.3f;

    private bool isFiring = false;

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
            //check if item can be hit
            if (hit.collider.GetComponent<TargetScript>() != null)
            {
                //if target can be hit, destroy
                hit.collider.GetComponent<TargetScript>().DoHit();

                //can show bullet hole image if hit wall/ground etc - can fade away after awhile
            }
        }

        //play particle animation
        this.GetComponentInChildren<ParticleSystem>().Play();

        //wait for cooldown
        yield return new WaitForSeconds(cooldown);

        isFiring = false;
    }
}
