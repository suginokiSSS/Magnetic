using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMove : MonoBehaviour
{
    public float targetRotation = 90f; // 目標の回転角度
    public float rotationSpeed = 50f; // 回転速度

    private float currentRotation = 0f; // 現在の回転角度

    private bool targetplus = true;

    public bool dontMove = false;

    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
        if (targetRotation < 0)
        {
            targetplus = false;
            rotationSpeed = rotationSpeed * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetplus == true)
        {
            // 衝突していない場合は目標の回転角度に到達するまで回転させる
            if (this.tag == "onswitch" && currentRotation <= targetRotation)
            {
                float rotationAmount = rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationAmount);
                currentRotation += rotationAmount;
            }
        }
        else
        {
            // 衝突していない場合は目標の回転角度に到達するまで回転させる
            if (this.tag == "onswitch" && currentRotation >= targetRotation)
            {
                float rotationAmount = rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationAmount);
                currentRotation += rotationAmount;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.tag != "onswitch" && dontMove == false)
        {
            if (other.gameObject.CompareTag("catch") || other.gameObject.CompareTag("fly"))
            {
                this.tag = "onswitch";

                soundManager.PlaySe(clip);
            }
        }
    }
}
