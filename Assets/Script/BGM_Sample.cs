using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Sample : MonoBehaviour
{
    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;

    void Start()
    {
        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
        soundManager.PlayBgm(clip);
        Application.targetFrameRate = 60;
    }
}
