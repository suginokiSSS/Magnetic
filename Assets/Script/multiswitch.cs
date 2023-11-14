using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multiswitch : MonoBehaviour
{
    public GameObject[] isswitch;
    private int count = 0;
    private int Maxcount = 0;
    // Start is called before the first frame update
    void Start()
    {
        Maxcount = isswitch.Length;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < isswitch.Length; i++)
        {
            if(isswitch[i].tag == "onswitch")
            {
                count++;
            }
        }

        if(count >= Maxcount)
        {
            Destroy(this.gameObject);
        }
        count = 0;
    }
}
