using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class enemy_generator : MonoBehaviour
{
    //自動生成かイベントか
    private bool eventenemy = false;
    //一度か複数か
    public bool onlyone = false;
    //敵プレハブ
    public GameObject enemyPrefab;
    //イベントオブジェクト
    public GameObject target;
    //ターゲットが死んだか
    private bool targetdestroy = false;
    //敵生成時間間隔
    public float interval = 5f;
    //経過時間
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
            //時間計測
            time += Time.deltaTime;
        }

        if (time > interval)
        {
            //enemyをインスタンス化する(生成する)
            GameObject enemy = Instantiate(enemyPrefab);
            //生成した敵の座標を決定する
            enemy.transform.position = this.transform.position;
            //経過時間を初期化して再度時間計測を始める
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
