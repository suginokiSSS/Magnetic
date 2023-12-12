using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_up : MonoBehaviour
{
    public Transform targetPosition; // �ړ���̈ʒu���w�肷�邽�߂�Transform�ϐ�

    public float moveSpeed = 5f; // �ړ����x��ݒ�i�I�v�V�����j
    private float time = 0f;

    public bool move = false;

    private float originspeed = 0f;
    player_move playerscript;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Player");
        playerscript = obj.GetComponent<player_move>();

        originspeed = playerscript.speed;
    }

    void Update()
    {
        if (this.tag == "onswitch")
        {
            time += Time.deltaTime;
            // �ړ���̈ʒu�Ɍ������Ĉړ����邽�߂̏���
            if (targetPosition != null)
            {
                float step = moveSpeed * Time.deltaTime; // �t���[�����[�g�ɂ�炸��葬�x�ňړ����邽�߂̒���

                // ���݂̈ʒu����ڕW�ʒu�Ɍ������Ĉړ�
                transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, step);
            }

            if (time >= 0.1f && time < 7f)
            {
                playerscript.speed = 0f;
                move = true;
            }
            else
            {
                playerscript.speed = originspeed;
                move = false;
            }
        }
    }
}
