using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    [Header("Stats")]
    public int health = 20;

    private void Start()
    {
        
    }

    //int = how much health reduced
    public void TakeDamage(int damage)
    {
        //minus health here
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
