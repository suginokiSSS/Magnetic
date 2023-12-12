using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Switch : MonoBehaviour
{
    public GameObject target;
    private bool once = false;

    [SerializeField]
    [Tooltip("切り替え後のカメラ")]
    private CinemachineVirtualCamera virtualCamera;

    // 切り替え後のカメラの元々のPriorityを保持しておく
    private int defaultPriority;

    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        defaultPriority = virtualCamera.Priority;

        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }   

    private void OnCollisionEnter(Collision other)
    {
        if (once == false)
        {
            if (other.gameObject.CompareTag("fly"))
            {
                Hit();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(once == false)
        {
            if (other.gameObject.CompareTag("catch"))
            {
                Hit();
            }
        }
    }

    void Hit()
    {
        once = true;

        target.tag = "onswitch";

        Invoke("MoveCamera", 1f);
    }

    void MoveCamera()
    {
        // 他のvirtualCameraよりも高い優先度にすることで切り替わる
        virtualCamera.Priority = 100;
        soundManager.PlaySe(clip);

        Invoke("MainCamera", 4f);
    }

    void MainCamera()
    {
        // 元のpriorityに戻す
        virtualCamera.Priority = defaultPriority;
    }   
}
