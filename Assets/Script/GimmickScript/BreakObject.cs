using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    public GameObject[] mono;
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

    private void OnCollisionEnter(Collision other)
    {
        if (only)
        {
            if (other.gameObject.CompareTag("fly") || other.gameObject.CompareTag("Player"))
            {
                int rnd = Random.Range(1, 3);
                for (int i = 0; i < rnd; i++)
                {
                    int rndmono = Random.Range(0, mono.Length);
                    GameObject newMono = Instantiate(mono[rndmono], transform.position, transform.rotation);
                }
                soundManager.PlaySe(clip);
                Destroy(this.gameObject);
                only = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (only)
        {
            if (other.gameObject.CompareTag("catch"))
            {
                int rnd = Random.Range(1, 3);
                for (int i = 0; i < rnd; i++)
                {
                    int rndmono = Random.Range(0, mono.Length);
                    GameObject newMono = Instantiate(mono[rndmono], transform.position, transform.rotation);
                }
                soundManager.PlaySe(clip);
                Destroy(this.gameObject);
                only = false;
            }
        }
    }
}
