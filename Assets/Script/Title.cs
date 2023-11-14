using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Title : MonoBehaviour
{
    private bool startinput = false;

    SoundManager soundManager;
    [SerializeField]
    AudioClip clip_start;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey && startinput == false)
        {
            startinput = true;
            soundManager.PlaySe(clip_start);
            Invoke("ChangeScene", 2f);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("Select");
    }
}
