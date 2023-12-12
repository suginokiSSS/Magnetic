using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2_catch : MonoBehaviour
{
    private bool startinput = false;
    public GameObject otherStage;

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
        if (this.tag == "catch" && startinput == false)
        {
            otherStage.tag = "Untagged";
            startinput = true;
            soundManager.PlaySe(clip_start);
            Invoke("ChangeScene", 2f);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("Stage2");
    }
}
