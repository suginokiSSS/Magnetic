using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_life : MonoBehaviour
{
    public GameObject[] mono;
    private bool dead = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("fly") && dead == false)
        {
            int rnd = Random.Range(2, 3);
            for (int i = 0; i < rnd; i++)
            {
                int rndmono = Random.Range(0, 3);
                GameObject newMono = Instantiate(mono[rndmono], transform.position, transform.rotation);
            }
            soundManager.PlaySe(clip);
            dead = true;
            Destroy(this.gameObject);
        }
    }
}
