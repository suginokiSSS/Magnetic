using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_life : MonoBehaviour
{
    public GameObject[] mono;
    public int monoMax = 3;
    private int monoCount = 0;
    private float time = 0;
    private bool dead = false;

    player_rotate playerrotate;

    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();

        obj = GameObject.Find("Player");
        playerrotate = obj.GetComponent<player_rotate>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("fly") && dead == false)
        {
            dead = true;
            int rnd = Random.Range(2, 4);
            for (int i = 0; i < rnd; i++)
            {
                int rndmono = Random.Range(0, mono.Length);
                GameObject newMono = Instantiate(mono[rndmono], transform.position, transform.rotation);
            }
            soundManager.PlaySe(clip);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("catch") && time > 1 && monoCount <= monoMax)
        {
            time = 0;
            monoCount++;
            int rndmono = Random.Range(0, mono.Length);
            GameObject newMono = Instantiate(mono[rndmono], transform.position, transform.rotation);

            playerrotate.Maxrotate += 30f;
        }
    }
}
