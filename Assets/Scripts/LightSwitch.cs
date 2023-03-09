using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    private bool PlayerInZone;
    public GameObject lightorobj;


    // Start is called before the first frame update
    private void Start()
    {
        PlayerInZone = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (PlayerInZone && Input.GetKeyDown(KeyCode.L))
        {
            lightorobj.SetActive(!lightorobj.activeSelf);
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animator>().Play("switch");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = false;
        }
    }
}
