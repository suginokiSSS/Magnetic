using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class enemy_generator : MonoBehaviour
{
    //�����������C�x���g��
    private bool eventenemy = false;
    //��x��������
    public bool onlyone = false;
    //�G�v���n�u
    public GameObject enemyPrefab;
    //�C�x���g�I�u�W�F�N�g
    public GameObject target;
    //�^�[�Q�b�g�����񂾂�
    private bool targetdestroy = false;
    //�G�������ԊԊu
    public float interval = 5f;
    //�o�ߎ���
    private float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if(target)
        {
            eventenemy = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (eventenemy == false)
        {
            //���Ԍv��
            time += Time.deltaTime;
        }

        if (time > interval)
        {
            //enemy���C���X�^���X������(��������)
            GameObject enemy = Instantiate(enemyPrefab);
            //���������G�̍��W�����肷��
            enemy.transform.position = this.transform.position;
            //�o�ߎ��Ԃ����������čēx���Ԍv�����n�߂�
            time = 0f;

            if(onlyone == true)
            {
                eventenemy = true;
            }
        }

        if(target == null && eventenemy == true && targetdestroy == false)
        {
            targetdestroy = true;
            time = 99f;
        }
    }
}
