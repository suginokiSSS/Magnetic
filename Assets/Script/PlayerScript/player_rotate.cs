using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_rotate : MonoBehaviour
{
    private Quaternion previousRotation;
    [HideInInspector] public float rightRotationValue = 0f; // �E��]���̒l
    [HideInInspector] public float leftRotationValue = 0f; // ����]���̒l
    [HideInInspector] public float Maxrotate = 0f;

    public bool autoRotation = true;

    private bool rightrotation = true;

    private Vector3 Player_pos; //�v���C���[�̃|�W�V����

    player_catch playercatch;

    Open_up openscript;

    private void Start()
    {
        Player_pos = GetComponent<Transform>().position; //�ŏ��̎��_�ł̃v���C���[�̃|�W�V�������擾
        previousRotation = transform.rotation;

        playercatch = GetComponent<player_catch>();

        GameObject obj = GameObject.Find("Door");
        openscript = obj.GetComponent<Open_up>();
    }

    private void FixedUpdate()
    {
        //if (openscript.move == true && autoRotation == true)
        //{
        //    Maxrotate ++;
        //}

        //if (Maxrotate >= 601f)
        //{
        //    Maxrotate = 600f;
        //}

        Vector3 diff = transform.position - Player_pos; //�v���C���[���ǂ̕����ɐi��ł��邩���킩��悤�ɁA�����ʒu�ƌ��ݒn�̍��W�������擾

        if (diff.magnitude > 0.01f && autoRotation == false) //�x�N�g���̒�����0.01f���傫���ꍇ�Ƀv���C���[�̌�����ς��鏈��������(0�ł͓���Ȃ��̂Łj
        {
            Quaternion lookRotation = Quaternion.LookRotation(diff, Vector3.up);
            transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
            Player_pos = transform.position; //�v���C���[�̈ʒu���X�V
        }

        Quaternion currentRotation = transform.rotation;
        float rotationDifference = Quaternion.Angle(previousRotation, currentRotation);
        previousRotation = currentRotation;

        if(playercatch.Checkthrow >= 1 && autoRotation == false)
        {
            autoRotation = true;
            //Maxrotate = 500f;
        }

        if (Input.GetKey(KeyCode.L) || Input.GetKey("joystick button 5"))
        {
            rightrotation = true;
            // y���𒆐S�ɉ�]������
            transform.Rotate(0, 300f * Time.deltaTime, 0);
            //Maxrotate -= 0.5f;
        }
        else if (Input.GetKey(KeyCode.J) || Input.GetKey("joystick button 4"))
        {
            rightrotation = false;
            // y���𒆐S�ɉ�]������
            transform.Rotate(0, -300f * Time.deltaTime, 0);
            //Maxrotate -= 0.5f;
        }

        if (playercatch.Checkthrow > 0)
        {
            if (rightrotation == true)
            {
                transform.Rotate(0, 200f * Time.deltaTime / ((float)playercatch.Checkthrow / 7 + 1), 0);
                //Maxrotate--;
            }
            else
            {
                // y���𒆐S�ɉ�]������
                transform.Rotate(0, -200f * Time.deltaTime / ((float)playercatch.Checkthrow / 7 + 1), 0);
                //Maxrotate--;
            }
        }

        //if (Maxrotate <= 0)
        //{
        //    autoRotation = false;
        //}

        if (Input.GetKey("joystick button 2") || Input.GetKey("joystick button 3") || Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.O))
        {
            Maxrotate = -100;
            autoRotation = false;
        }
    }
}
