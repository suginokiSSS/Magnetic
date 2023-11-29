using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMove : MonoBehaviour
{
    public float targetRotation = 90f; // �ڕW�̉�]�p�x
    public float rotationSpeed = 50f; // ��]���x

    private float currentRotation = 0f; // ���݂̉�]�p�x

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
            // �Փ˂��Ă��Ȃ��ꍇ�͖ڕW�̉�]�p�x�ɓ��B����܂ŉ�]������
            if (this.tag == "onswitch" && currentRotation < targetRotation)
            {
                float rotationAmount = rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationAmount);
                currentRotation += rotationAmount;
            }
        }
        else
        {
            // �Փ˂��Ă��Ȃ��ꍇ�͖ڕW�̉�]�p�x�ɓ��B����܂ŉ�]������
            if (this.tag == "onswitch" && currentRotation > targetRotation)
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
