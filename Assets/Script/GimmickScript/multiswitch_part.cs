using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multiswitch_part : MonoBehaviour
{
    private float time = 0f;

    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time >= 0)
        {
            this.tag = "onswitch";
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            this.tag = "Untagged";
            GetComponent<Renderer>().material.color = Color.red;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("catch"))
        {
            if(time <= 0f)
            {
                soundManager.PlaySe(clip);
            }
            time = 0.2f;
        }
    }
}
