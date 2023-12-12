using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject_fly : MonoBehaviour
{
    private bool only = true;

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
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (only == true)
        {
            if (other.gameObject.CompareTag("fly"))
            {
                soundManager.PlaySe(clip);
                Destroy(this.gameObject);
                only = false;
            }
        }
    }
}
