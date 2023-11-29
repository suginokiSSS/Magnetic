using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mono_catch : MonoBehaviour
{
    player_catch playerscript;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Player");
        playerscript = obj.GetComponent<player_catch>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.tag == "catch" && playerscript.Checkthrow >= 1)
        {
            if (other.tag == "throw")
            {
                         
            }
        }
    }
}
