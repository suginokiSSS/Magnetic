using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_up : MonoBehaviour
{
    public Transform targetPosition; // 移動先の位置を指定するためのTransform変数

    public float moveSpeed = 5f; // 移動速度を設定（オプション）
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
            // 移動先の位置に向かって移動するための処理
            if (targetPosition != null)
            {
                float step = moveSpeed * Time.deltaTime; // フレームレートによらず一定速度で移動するための調整

                // 現在の位置から目標位置に向かって移動
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
