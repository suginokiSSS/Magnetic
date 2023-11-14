using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_jump : MonoBehaviour
{
    //�W�����v�̑��x��ݒ�
    public float _velocity = 5.0f;

    private Rigidbody _rigidbody;
    //���n��Ԃ��Ǘ�
    private bool _isGrounded;

    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;

    void Start()
    {
        //Rigidbody�R���|�[�l���g���擾
        _rigidbody = GetComponent<Rigidbody>();
        //�ŏ��͒��n���Ă��Ȃ����
        _isGrounded = false;

        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
    }

    void Update()
    {
        //���n���Ă��邩�𔻒�
        if (_isGrounded == true)
        {
            //�X�y�[�X�L�[��������Ă��邩�𔻒�
            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                //�W�����v�̕�����������̃x�N�g���ɐݒ�
                Vector3 jump_vector = Vector3.up;
                //�W�����v�̑��x���v�Z
                Vector3 jump_velocity = jump_vector * _velocity;

                //������̑��x��ݒ�
                _rigidbody.velocity = jump_velocity;
                //�n�ʂ��痣���̂Œ��n��Ԃ���������
                _isGrounded = false;

                soundManager.PlaySe(clip);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //���n�����o�����̂Œ��n��Ԃ���������
        _isGrounded = true;
    }
}
