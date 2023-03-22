using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public bool destroyOnHit;

    //[Header("Effects")]
    //public GameObject muzzleEffect;
    //public GameObject hitEffect;

    [Header("Explosives")]
    public bool isExplosive;
    public float explostionRadius;
    public float explosionForce;
    public int explosionDamage;
    public GameObject explosionEffect;

    private Rigidbody rb;

    private bool hitTarget;

    private GameObject floor;

    // Start is called before the first frame update
    void Start()
    {
        //get rigidbody component
        rb = GetComponent<Rigidbody>();

        //spawn muzzleEffect if assigned
        //if (muzzleEffect != null)
        //{
        //    Instantiate(muzzleEffect, transform.position, Quaternion.identity);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitTarget)
            return;
        else
            hitTarget = true;

        //enemy hit
        TargetScript enemy = collision.gameObject.GetComponent<TargetScript>();

        //deal damage to enemy
        enemy.TakeDamage(damage);

        //spawn hit effect if assigned
        //if (hitEffect != null)
        //{
        //    Instantiate(hitEffect, transform.position, Quaternion.identity);
        //}

        //destroy projectile
        if (!isExplosive && destroyOnHit)
        {
            Invoke(nameof(DestroyProjectile), 0.1f);
        }

        //explode projectile if explosive
        if (isExplosive)
        {
            Explode();
            return;
        }

        //  ------------    ONLY IF USING ITEMS THAT STICKS TO STUFF    --------------
        ////make sure projectile sticks to surface
        //rb.isKinematic = true;

        ////make sure projectile moves with target
        //transform.SetParent(collision.transform);
    }

    private void Explode()
    {
        //spawn explosion effect is assigned
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        //find all object inside explosion range
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, explostionRadius);

        //loop through all found objects and apply damage + explosion force
        for (int i = 0; i < objectsInRange.Length; i++)
        {
            if (objectsInRange[i].gameObject == gameObject)
            {
                //dont break or return
            }
            else
            {
                //check if object == enemy, if yes, deal explosionDamage
                if (objectsInRange[i].GetComponent<TargetScript>() != null)
                {
                    objectsInRange[i].GetComponent<TargetScript>().TakeDamage(explosionDamage);
                }

                //check if object has rigidbody
                if (objectsInRange[i].GetComponent<Rigidbody>() != null)
                {
                    //custom explosiveForce
                    Vector3 objectPos = objectsInRange[i].transform.position;

                    //calculate force direction
                    Vector3 forceDirection = (objectPos - transform.position).normalized;

                    //apply force to object in range
                    objectsInRange[i].GetComponent<Rigidbody>().AddForceAtPosition(forceDirection * explosionForce + Vector3.up * explosionForce, objectPos);

                    Debug.Log("BOOM" + objectsInRange[i].name);
                }
            }
        }

        //destroy projectile with 0.1 second delay
        Invoke(nameof(DestroyProjectile), 0.1f);

    }

    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
