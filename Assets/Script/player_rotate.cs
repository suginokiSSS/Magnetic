using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class player_rotate : MonoBehaviour
{
    private Quaternion previousRotation;
    private float rotationThreshold = 0.1f; // ��]�����臒l�i�����ȉ�]�𖳎����邽�߂̒l�j
    [HideInInspector] public float rightRotationValue = 0f; // �E��]���̒l
    [HideInInspector] public float leftRotationValue = 0f; // ����]���̒l
    private float stopRotationValue = 0f; //�~�܂��Ă���Ƃ��̒l
    private float time = 0f; //����

    public bool autoRotation = false;

    private Vector3 Player_pos; //�v���C���[�̃|�W�V����

    [SerializeField]
    [Tooltip("y���̉�]�p�x")]
    private float rotateY = 450;

    player_catch playercatch;

    private void Start()
    {
        Player_pos = GetComponent<Transform>().position; //�ŏ��̎��_�ł̃v���C���[�̃|�W�V�������擾
        previousRotation = transform.rotation;

        playercatch = GetComponent<player_catch>();
    }

    private void FixedUpdate()
    {
        Vector3 diff = transform.position - Player_pos; //�v���C���[���ǂ̕����ɐi��ł��邩���킩��悤�ɁA�����ʒu�ƌ��ݒn�̍��W�������擾

        if (diff.magnitude > 0.01f && autoRotation == false) //�x�N�g���̒�����0.01f���傫���ꍇ�Ƀv���C���[�̌�����ς��鏈��������(0�ł͓���Ȃ��̂Łj
        {
            Quaternion lookRotation = Quaternion.LookRotation(diff, Vector3.up);
            transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
        }

        Player_pos = transform.position; //�v���C���[�̈ʒu���X�V

        Quaternion currentRotation = transform.rotation;
        float rotationDifference = Quaternion.Angle(previousRotation, currentRotation);

        // ��]��臒l�ȏ�ł���ꍇ�ɉ�]�����𔻕�
        if (rotationDifference > rotationThreshold)
        {
            Vector3 previousForward = previousRotation * Vector3.forward;
            Vector3 currentForward = currentRotation * Vector3.forward;
            Vector3 crossProduct = Vector3.Cross(previousForward, currentForward);

            // �x�N�g���̊O�ς𗘗p���ĉ�]�����𔻕�
            if (crossProduct.y > 0)
            {
                Debug.Log("�E��]�l: " + rightRotationValue);
                rightRotationValue += Time.deltaTime * 50;
                stopRotationValue = 0;
                // �E��]���̏����������ɒǉ�
            }
            else if (crossProduct.y < 0)
            {
                Debug.Log("����]�l: " + leftRotationValue);
                leftRotationValue += Time.deltaTime * 50;
                stopRotationValue = 0;
                // ����]���̏����������ɒǉ�
            }
        }
        else
        {
            Debug.Log("�Î~�l" + stopRotationValue);
            stopRotationValue++;
            // �Î~���̏����������ɒǉ�
        }

        if (rightRotationValue >= 85f && time < 7)
        {
            // Y���ɑ΂��āA�w�肵���p�x����]�����Ă���B
            gameObject.transform.Rotate(new Vector3(0, rotateY, 0) * Time.deltaTime / (playercatch.Checkthrow / 5 + 1));
            time += Time.deltaTime;
            leftRotationValue = 0;
            autoRotation = true;
        }
        else if (leftRotationValue >= 85f && time < 7)
        {
            // Y���ɑ΂��āA�w�肵���p�x����]�����Ă���B
            gameObject.transform.Rotate(new Vector3(0, -rotateY, 0) * Time.deltaTime / (playercatch.Checkthrow / 5 + 1));
            time += Time.deltaTime;
            rightRotationValue = 0;
            autoRotation = true;
        }
        else if (rightRotationValue < 10 && leftRotationValue < 10)
        {
            time = 0;
            autoRotation = false;
        }

        if(rightRotationValue > 10)
        {
            leftRotationValue = 0;
        }
        else if (leftRotationValue > 10)
        {
            rightRotationValue = 0;
        }

        if (time == 7 || Input.GetKey("joystick button 0") || Input.GetKey(KeyCode.K))
        {
            time = 8;
            rightRotationValue = 0;
            leftRotationValue = 0;
            autoRotation = false;
        }

        if (stopRotationValue > 10)
        {
            rightRotationValue = 0;
            leftRotationValue = 0;
        }

        previousRotation = currentRotation;
    }
}
