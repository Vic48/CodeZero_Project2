using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    public Throwing throwScript;
    public Projectile projectileScript;
    public WeaponFire firingScript;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        //  ----------------    USING SCROLLWHEEL   ---------------
        //if (Input.GetAxis("Mouse ScrollWheel") > 0)
        //{
        //    if (selectedWeapon >= transform.childCount - 1)
        //    {
        //        selectedWeapon = 0;
        //    }
        //    else
        //    {
        //        selectedWeapon++;
        //    }
        //}

        //if (Input.GetAxis("Mouse ScrollWheel") < 0)
        //{
        //    if (selectedWeapon <= 0)
        //    {
        //        selectedWeapon = transform.childCount - 1;
        //    }
        //    else
        //    {
        //        selectedWeapon--;
        //    }
        //}

        //  ----------------    USING NUMBER KEYS   ---------------
        //shield
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
            throwScript.readyToThrow = false;
            throwScript.readyToFire = false;
        }

        //pistol
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
            //Debug.Log("pistol");
            throwScript.readyToThrow = false;
            throwScript.readyToFire = true;
        }

        //grenade
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
            throwScript.readyToThrow = true;
            throwScript.readyToFire = false;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    public void SelectWeapon()
    {
        //weapon index
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
