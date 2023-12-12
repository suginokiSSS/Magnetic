using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class multiswitch : MonoBehaviour
{
    public GameObject[] isswitch;
    private int count = 0;

    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.yellow;

        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(count >= isswitch.Length)
        {
            GetComponent<Renderer>().material.color = Color.green;
            this.tag = "onswitch";
            count = 0;
        }

        count = 0;

        for (int i = 0; i < isswitch.Length; i++)
        {
            if(isswitch[i].tag == "onswitch")
            {
                count++;
            }
        }
    }
}
