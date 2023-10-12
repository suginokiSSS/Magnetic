using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour
{
    private Transform target;  // プレイヤーのTransform
    public GameObject player; // プレイヤーオブジェクト
    public Transform turretHead; // 砲台の頭部のTransform
    public GameObject bulletPrefab; // 弾のプレハブ
    public Transform bulletSpawnPoint; // 弾の発射位置
    public float bulletSpeed = 50f; // 弾の速度
    public float fireInterval = 6f; // 発射間隔
    public float stopDistance = 0f;
    public float moveDistance = 30f;
    public GameObject chargeEffect;
    public GameObject fireEffect;

    private bool fire = true;
    private bool shoteff = false;

    private float timer; // タイマー

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
            // プレイヤーの方向を向く
            Vector3 direction = player.transform.position - turretHead.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            if (fire == true)
            {
                turretHead.rotation = Quaternion.Lerp(turretHead.rotation, targetRotation, Time.deltaTime * 5f);
            }

            // タイマーの更新と発射処理
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
        // 弾の発射処理
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // 弾の速度と方向を設定
        Vector3 shootDirection = (player.transform.position - bulletSpawnPoint.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;

        // 砲弾を破壊する
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

    //エフェクトを生成する
    void GenerateFireEffect()
    {
        //エフェクトを生成する
        GameObject effect = Instantiate(fireEffect) as GameObject;
        //エフェクトが発生する場所を決定する
        effect.transform.position = gameObject.transform.position;

        Destroy(effect.gameObject, 3.0f);
    }

    void GenerateChargeEffect()
    {
        //エフェクトを生成する
        GameObject effect = Instantiate(chargeEffect) as GameObject;
        //エフェクトが発生する場所を決定する
        effect.transform.position = gameObject.transform.position;

        Destroy(effect.gameObject, 3.0f);
    }
}
