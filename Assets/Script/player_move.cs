using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    public float speed = 5f; //�v���C���[�̓����X�s�[�h

    private Vector3 Player_pos; //�v���C���[�̃|�W�V����
    private float x; //x������Imput�̒l
    private float z; //z������Input�̒l
    private Rigidbody rigd;
    private Animator animator;

    void Start()
    {
        Player_pos = GetComponent<Transform>().position; //�ŏ��̎��_�ł̃v���C���[�̃|�W�V�������擾
        rigd = GetComponent<Rigidbody>(); //�v���C���[��Rigidbody���擾
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        x = Input.GetAxis("Horizontal"); //x������Input�̒l���擾
        z = Input.GetAxis("Vertical"); //z������Input�̒l���擾

        rigd.velocity = new Vector3(x * speed, 0, z * speed); //�v���C���[��Rigidbody�ɑ΂���Input��speed���|�����l�ōX�V���ړ�

        Player_pos = transform.position; //�v���C���[�̈ʒu���X�V
    }
}
