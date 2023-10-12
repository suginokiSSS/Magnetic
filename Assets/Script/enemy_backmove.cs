using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_backmove : MonoBehaviour
{
    private Transform target;  // �v���C���[��Transform
    public Rigidbody rb;

    public float moveSpeed = 3f;  // �G�̈ړ����x
    public float rotationSpeed = 3f;  // �G�̉�]���x
    public GameObject bulletPrefab; // �e�̃v���n�u
    public Transform bulletSpawnPoint; // �e�̔��ˈʒu
    public float bulletSpeed = 50f; // �e�̑��x
    public float fireInterval = 5f; // ���ˊԊu
    public float stopDistance = 0f;
    public float moveDistance = 20f;
    private bool fire = true;

    [HideInInspector] public bool isStunned = false;  // �G���U�����󂯂Ă��邩�ǂ����̃t���O
    private float stunDuration = 3f;  // �U�����󂯂Ē�~���鎞�Ԃ̒���
    private float stunTimer = 0f;  // �U�����󂯂Čo�߂�������
    private float timer; // �^�C�}�[

    public AudioClip audiostunned;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        timer = fireInterval;
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

        // �v���C���[�̕���������
        if (isStunned == false)
        {
            Vector3 direction = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            if (distance < moveDistance && distance > stopDistance)
            {
                // �v���C���[�Ɍ������ė����
                transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
            }

            // �^�C�}�[�̍X�V�Ɣ��ˏ���
            timer -= Time.deltaTime;
            if (timer <= 0f && fire == true)
            {
                Fire();
                timer = fireInterval;
            }

            if (timer <= 0f && fire == false)
            {
                fire = true;
                timer = fireInterval;
            }
        }
    }

    private void Fire()
    {
        // �e�̔��ˏ���
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // �e�̑��x�ƕ�����ݒ�
        Vector3 shootDirection = (target.transform.position - bulletSpawnPoint.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;

        // �C�e��j�󂷂�
        Destroy(bullet, 3.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("catch"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (!isStunned)
        {
            // �U�����󂯂��ꍇ�A�X�^�����J�n���A�͂�����΂�
            isStunned = true;
            stunTimer = 0f;

            // �Q�[���I�u�W�F�N�g��y����180�x��]������
            transform.Rotate(0, 180, 0);

            AudioSource.PlayClipAtPoint(audiostunned, transform.position);
        }
    }
}
