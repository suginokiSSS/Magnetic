using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Switch_Bar : MonoBehaviour
{
    public GameObject target;
    public bool dontmove = true;
    private bool once = false;

    public float time_one = 2f;
    public float time_two = 2f;

    [SerializeField]
    [Tooltip("切り替え後のカメラ")]
    private CinemachineVirtualCamera virtualCamera;

    // 切り替え後のカメラの元々のPriorityを保持しておく
    private int defaultPriority;

    private float originspeed = 0f;
    player_move playerscript;

    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        defaultPriority = virtualCamera.Priority;

        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();

        obj = GameObject.Find("Player");
        playerscript = obj.GetComponent<player_move>();

        originspeed = playerscript.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (once == false)
        {
            if (this.tag == "onswitch")
            {
                if (dontmove == true)
                {
                    playerscript.speed = 0f;
                }
                once = true;
                Invoke("MoveCamera", 0.3f);
            }
        }
    }

    void MoveCamera()
    {
        // 他のvirtualCameraよりも高い優先度にすることで切り替わる
        virtualCamera.Priority = 50;

        soundManager.PlaySe(clip);

        Invoke("MoveBar", time_one);
    }

    void MoveBar()
    {
        target.tag = "onswitch";

        Invoke("MainCamera", time_two);
    }

    void MainCamera()
    {
        // 元のpriorityに戻す
        virtualCamera.Priority = defaultPriority;
        playerscript.speed = originspeed;
    }
}
