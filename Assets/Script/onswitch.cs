using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onswitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("catch") || other.gameObject.CompareTag("fly"))
        {
            this.tag = "onswitch";
        }
        else
        {
            this.tag = "Untagged";
        }
    }
}
