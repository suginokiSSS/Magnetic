using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_catch : MonoBehaviour
{
    public float throwForce = 15f;  // �������

    public float grabDistance = 5f; // �͂ދ���

    private Rigidbody[] objectToThrow; // ������I�u�W�F�N�g��Rigidbody

    public int Checkthrow = 0;

    [SerializeField]
    [Tooltip("�e1")]
    private GameObject bullet1;

    player_rotate playerrotate;

    void Start()
    {
        playerrotate = GetComponent<player_rotate>();
        objectToThrow = new Rigidbody[30];
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E) && playerrotate.autoRotation == true)
        {
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }

        //��̈ʒu����͂ދ����ȓ��ɂ���tag��"throw"�̃I�u�W�F�N�g���擾
        if (objectToThrow.Length == 0)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, grabDistance);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "throw")
                {
                    objectToThrow[Checkthrow] = collider.attachedRigidbody;
                    Checkthrow++;
                    collider.tag = "catch";
                    return;
                }
            }
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, grabDistance);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "throw")
                {
                    objectToThrow[Checkthrow] = collider.attachedRigidbody;
                    Checkthrow++;
                    collider.tag = "catch";
                    return;
                }
            }
        }

        if (playerrotate.autoRotation == true)
        {
            // K�{�^�������ƃI�u�W�F�N�g�𓊂���
            if (Input.GetKey("joystick button 0") || Input.GetKey(KeyCode.K))
            {
                if (objectToThrow[0] != null)
                {
                    GameObject newBullet = Instantiate(bullet1, this.transform.position, transform.rotation);
                    // ������������v�Z
                    Vector3 throwDirection = transform.forward;
                    throwDirection.y = 0f;
                    // ��̈ʒu����ɁA�I�u�W�F�N�g�𓊂���
                    newBullet.GetComponent<Rigidbody>().AddForce(throwDirection * throwForce, ForceMode.Impulse);

                    // �e���������ꂽ�u�Ԃ̃X�P�[����ۑ�
                    Vector3 initialScale = newBullet.transform.localScale;

                    // �e����������Ă���̌o�ߎ��Ԃ��v��
                    float elapsedTime = 0f;

                    // �e����������Ă���3�b�Ԃ͑傫���Ȃ�
                    while (elapsedTime < 3f)
                    {
                        // ���Ԍo�߂����Z
                        elapsedTime += Time.deltaTime;

                        // �e�̃X�P�[����ύX���đ傫������
                        newBullet.transform.localScale = initialScale * (1 + Checkthrow);
                    }

                        Destroy(newBullet, 1f);
                }

                if (Checkthrow >= 1)
                {
                    for (int i = 0; i < Checkthrow; i++)
                    {
                        Destroy(objectToThrow[i].gameObject);
                        objectToThrow[i] = null;
                    }
                    Checkthrow = 0;
                }
            }

        }

        // �I�u�W�F�N�g������ł���Ԃ́A��̈ʒu�ɍ��킹��
        for (int i = 0; i < Checkthrow; i++)
        {
            if (objectToThrow[i])
            {
                Vector3 throwDirection = this.transform.forward;
                throwDirection.y = 0f;
                objectToThrow[i].transform.position = this.transform.position + throwDirection * (i + 1) * 2;
            }
        }
    }
}
