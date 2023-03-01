using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    //mayb can give health to targets

    public void DoHit()
    {
        Destroy(this.gameObject);
    }
}
