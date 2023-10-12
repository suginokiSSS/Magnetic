using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour
{
    private Transform target;  // �v���C���[��Transform
    public GameObject player; // �v���C���[�I�u�W�F�N�g
    public Transform turretHead; // �C��̓�����Transform
    public GameObject bulletPrefab; // �e�̃v���n�u
    public Transform bulletSpawnPoint; // �e�̔��ˈʒu
    public float bulletSpeed = 50f; // �e�̑��x
    public float fireInterval = 6f; // ���ˊԊu
    public float stopDistance = 0f;
    public float moveDistance = 30f;
    public GameObject chargeEffect;
    public GameObject fireEffect;

    private bool fire = true;
    private bool shoteff = false;

    private float timer; // �^�C�}�[

    public GameObject[] mono;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        timer = fireInterval;
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < moveDistance && distance > stopDistance)
        {
            // �v���C���[�̕���������
            Vector3 direction = player.transform.position - turretHead.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            if (fire == true)
            {
                turretHead.rotation = Quaternion.Lerp(turretHead.rotation, targetRotation, Time.deltaTime * 5f);
            }

            // �^�C�}�[�̍X�V�Ɣ��ˏ���
            timer -= Time.deltaTime;
            if (timer <= 0f && fire == true)
            {
                Fire();
                timer = fireInterval;
                shoteff = false;
            }

            if (timer <= 0f && fire == false)
            {
                fire = true;
                timer = fireInterval;
            }

            if (fireInterval - timer >= 2f && shoteff == false)
            {
                GenerateChargeEffect();
                shoteff = true;
            }
        }
    }

    private void Fire()
    {
        // �e�̔��ˏ���
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // �e�̑��x�ƕ�����ݒ�
        Vector3 shootDirection = (player.transform.position - bulletSpawnPoint.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;

        // �C�e��j�󂷂�
        Destroy(bullet, 2.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("catch"))
        {
            fire = false;
            timer = fireInterval;
            GenerateFireEffect();
        }

        if (other.gameObject.CompareTag("fly"))
        {
            int rnd = Random.Range(2, 3);
            for (int i = 0; i < rnd; i++)
            {
                int rndmono = Random.Range(0, 3);
                GameObject newMono = Instantiate(mono[rndmono], transform.position, transform.rotation);
            }
            Destroy(this.gameObject);
        }
    }

    //�G�t�F�N�g�𐶐�����
    void GenerateFireEffect()
    {
        //�G�t�F�N�g�𐶐�����
        GameObject effect = Instantiate(fireEffect) as GameObject;
        //�G�t�F�N�g����������ꏊ�����肷��
        effect.transform.position = gameObject.transform.position;

        Destroy(effect.gameObject, 3.0f);
    }

    void GenerateChargeEffect()
    {
        //�G�t�F�N�g�𐶐�����
        GameObject effect = Instantiate(chargeEffect) as GameObject;
        //�G�t�F�N�g����������ꏊ�����肷��
        effect.transform.position = gameObject.transform.position;

        Destroy(effect.gameObject, 3.0f);
    }
}
