using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open : MonoBehaviour
{
    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;

    public GameObject target;

    private bool opendoor = false;
    // Start is called before the first frame update
    void Start()
    {
        if(target != null)
        {
            opendoor= true;
        }

        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
    }

    void Update()
    {
        if(target == null && opendoor == true)
        {
            soundManager.PlaySe(clip);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("fly") && opendoor == false)
        {
            soundManager.PlaySe(clip);
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
