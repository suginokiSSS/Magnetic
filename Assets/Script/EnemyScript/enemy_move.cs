using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move : MonoBehaviour
{
    private Transform target;  // �v���C���[��Transform
    public Rigidbody rb;

    public float moveSpeed = 3f;  // �G�̈ړ����x
    public float rotationSpeed = 3f;  // �G�̉�]���x
    public float knockbackForce = 5f;  // �U���ɂ��͂�����΂��̗�
    public float stopDistance = 0f;
    public float moveDistance = 30f;

    [HideInInspector] public bool isStunned = false;  // �G���U�����󂯂Ă��邩�ǂ����̃t���O
    [HideInInspector] public bool canmove = false;
    public float stunDuration = 1f;  // �U�����󂯂Ē�~���鎞�Ԃ̒���
    private float stunTimer = 0f;  // �U�����󂯂Čo�߂�������

    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;

    private void Start()
    {     
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();

        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            // �v���C���[�����Ȃ��ꍇ�͏������I��
            return;
        }

        if (isStunned)
        {
            // �U�����󂯂Ă���Ԃ͏������s�킸�A�^�C�}�[���X�V����
            stunTimer += Time.deltaTime;
            if (stunTimer >= stunDuration)
            {
                // �X�^�����I��������Ăшړ��Ɖ�]���J�n����
                isStunned = false;
                stunTimer = 0f;
                rb.velocity = Vector3.zero;
            }
            else
            {
                // �X�^�����͒�~���Ă���
                return;
            }
        }

        // �ϐ� distance ���쐬���ăI�u�W�F�N�g�̈ʒu�ƃ^�[�Q�b�g�I�u�W�F�N�g�̋������i�[
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < moveDistance && distance > stopDistance)
        {
            canmove = true;
            moveDistance = 50f;
            // �v���C���[�̕���������
            Vector3 direction = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            // �v���C���[�Ɍ������Ĉړ�����
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            canmove = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("catch"))
        {
            // "catch"�^�O���t���Ă���I�u�W�F�N�g�ɂԂ������ꍇ�A�͂�����΂�
            Vector3 knockbackDirection = (-target.transform.position + transform.position).normalized;
            
            TakeDamage(knockbackDirection);
        }
    }

    public void TakeDamage(Vector3 knockbackDirection)
    {
        if (!isStunned)
        {
            // �U�����󂯂��ꍇ�A�X�^�����J�n���A�͂�����΂�
            isStunned = true;
            canmove = false;
            stunTimer = 0f;
           
            // �͂�����΂��̗͂�K�p
            rb.velocity = Vector3.zero;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

            soundManager.PlaySe(clip);
        }
    }
}
