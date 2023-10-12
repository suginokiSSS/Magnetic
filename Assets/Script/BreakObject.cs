using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    public GameObject[] mono;
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
        if (other.gameObject.CompareTag("catch") || other.gameObject.CompareTag("fly"))
        {
            int rnd = Random.Range(1, 3);
            for (int i = 0; i < rnd; i++)
            {
                int rndmono = Random.Range(0, 3);
                GameObject newMono = Instantiate(mono[rndmono], transform.position, transform.rotation);
            }
            Destroy(this.gameObject);
        }
    }
}
