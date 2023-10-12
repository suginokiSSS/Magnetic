using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Sample : MonoBehaviour
{
    [SerializeField]
    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;

    void Start()
    {
        soundManager.PlayBgm(clip);
    }
}
