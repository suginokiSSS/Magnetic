using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bomb : MonoBehaviour
{
    public GameObject bombObject;

    enemy_move enemyscript;
    // Start is called before the first frame update
    void Start()
    {
        enemyscript = GetComponent<enemy_move>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyscript.isStunned == true)
        {
            Invoke("Bomb", 1.0f);
        }
    }

    void Bomb()
    {
        //enemyをインスタンス化する(生成する)
        GameObject bakuhatu = Instantiate(bombObject);
        //生成した敵の座標を決定する
        bakuhatu.transform.position = this.transform.position;
        //死亡
        Destroy(bakuhatu, 0.5f);
        Destroy(this.gameObject);
    }
}
