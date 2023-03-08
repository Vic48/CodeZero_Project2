using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoleScript : MonoBehaviour
{
    [SerializeField] private GameObject _bulletHolePrefab; //Bullet hole

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //returns true if we click the mouse button
            RaycastHit hitInfo; //Contains raycast hit infomation
            if (Physics.Raycast(origin: transform.position, direction: transform.forward, out hitInfo))
            {
                //returns true if ray touches something
                //GameObject obj = Instantiate(_bulletHolePrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                GameObject obj = Instantiate(_bulletHolePrefab, hitInfo.point + (hitInfo.normal * 0.01f), Quaternion.identity);
                //Instantiating the bullet hole object
                //obj.transform.position += obj.transform.forward / 1000;
                obj.transform.LookAt(hitInfo.point, hitInfo.normal);
                //Changing the bullet hole's position a bit so it will fit better
            }
        }
    }
}
