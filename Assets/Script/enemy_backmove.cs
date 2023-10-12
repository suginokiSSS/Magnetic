using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_backmove : MonoBehaviour
{
    private Transform target;  // プレイヤーのTransform
    public Rigidbody rb;

    public float moveSpeed = 3f;  // 敵の移動速度
    public float rotationSpeed = 3f;  // 敵の回転速度
    public GameObject bulletPrefab; // 弾のプレハブ
    public Transform bulletSpawnPoint; // 弾の発射位置
    public float bulletSpeed = 50f; // 弾の速度
    public float fireInterval = 5f; // 発射間隔
    public float stopDistance = 0f;
    public float moveDistance = 20f;
    private bool fire = true;

    [HideInInspector] public bool isStunned = false;  // 敵が攻撃を受けているかどうかのフラグ
    private float stunDuration = 3f;  // 攻撃を受けて停止する時間の長さ
    private float stunTimer = 0f;  // 攻撃を受けて経過した時間
    private float timer; // タイマー

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
            // プレイヤーがいない場合は処理を終了
            return;
        }

        if (isStunned)
        {
            // 攻撃を受けている間は処理を行わず、タイマーを更新する
            stunTimer += Time.deltaTime;
            if (stunTimer >= stunDuration)
            {
                // スタンが終了したら再び移動と回転を開始する
                isStunned = false;
                stunTimer = 0f;
                rb.velocity = Vector3.zero;
            }
            else
            {
                // スタン中は停止している
                return;
            }
        }

        // 変数 distance を作成してオブジェクトの位置とターゲットオブジェクトの距離を格納
        float distance = Vector3.Distance(transform.position, target.position);

        // プレイヤーの方向を向く
        if (isStunned == false)
        {
            Vector3 direction = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            if (distance < moveDistance && distance > stopDistance)
            {
                // プレイヤーに向かって離れる
                transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
            }

            // タイマーの更新と発射処理
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
        // 弾の発射処理
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // 弾の速度と方向を設定
        Vector3 shootDirection = (target.transform.position - bulletSpawnPoint.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;

        // 砲弾を破壊する
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
            // 攻撃を受けた場合、スタンを開始し、はじき飛ばす
            isStunned = true;
            stunTimer = 0f;

            // ゲームオブジェクトをy軸で180度回転させる
            transform.Rotate(0, 180, 0);

            AudioSource.PlayClipAtPoint(audiostunned, transform.position);
        }
    }
}
