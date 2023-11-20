using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Player : MonoBehaviour
{
    private GameObject startpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fall")
        {
            this.gameObject.SetActive(false);
            Invoke("Retry", 0.3f);
        }

        if (other.gameObject.tag == "RetryPoint")
        {
            startpoint = other.gameObject;
            other.tag = "Untagged";
        }
    }

    void Retry()
    {
        this.transform.position = startpoint.transform.position;

        this.gameObject.SetActive(true);
    }
}
