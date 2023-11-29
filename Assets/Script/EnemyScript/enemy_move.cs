using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move : MonoBehaviour
{
    private Transform target;  // プレイヤーのTransform
    public Rigidbody rb;

    public float moveSpeed = 3f;  // 敵の移動速度
    public float rotationSpeed = 3f;  // 敵の回転速度
    public float knockbackForce = 5f;  // 攻撃によるはじき飛ばしの力
    public float stopDistance = 0f;
    public float moveDistance = 30f;

    [HideInInspector] public bool isStunned = false;  // 敵が攻撃を受けているかどうかのフラグ
    [HideInInspector] public bool canmove = false;
    public float stunDuration = 1f;  // 攻撃を受けて停止する時間の長さ
    private float stunTimer = 0f;  // 攻撃を受けて経過した時間

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

        if (distance < moveDistance && distance > stopDistance)
        {
            canmove = true;
            moveDistance = 50f;
            // プレイヤーの方向を向く
            Vector3 direction = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            // プレイヤーに向かって移動する
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
            // "catch"タグが付いているオブジェクトにぶつかった場合、はじき飛ばす
            Vector3 knockbackDirection = (-target.transform.position + transform.position).normalized;
            
            TakeDamage(knockbackDirection);
        }
    }

    public void TakeDamage(Vector3 knockbackDirection)
    {
        if (!isStunned)
        {
            // 攻撃を受けた場合、スタンを開始し、はじき飛ばす
            isStunned = true;
            canmove = false;
            stunTimer = 0f;
           
            // はじき飛ばしの力を適用
            rb.velocity = Vector3.zero;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

            soundManager.PlaySe(clip);
        }
    }
}
