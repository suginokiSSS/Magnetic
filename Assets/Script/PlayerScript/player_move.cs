using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    public float speed = 5f; //プレイヤーの動くスピード
    private float originspeed;
    public float jumpForce = 10f; //ジャンプの力

    private Vector3 Player_pos; //プレイヤーのポジション
    private float x; //x方向のImputの値
    private float z; //z方向のInputの値
    private Rigidbody rigd;
    private Animator animator;

    Open_up openscript;

    void Start()
    {
        originspeed = speed;
        Player_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
        rigd = GetComponent<Rigidbody>(); //プレイヤーのRigidbodyを取得
        animator = GetComponent<Animator>();

        GameObject obj = GameObject.Find("Door");
        openscript = obj.GetComponent<Open_up>();
    }

    void FixedUpdate()
    {
        if (openscript.move == true) 
        {
            speed = 0;
        }
        else
        {
            speed = originspeed;
        }

        x = Input.GetAxis("Horizontal"); //x方向のInputの値を取得
        z = Input.GetAxis("Vertical"); //z方向のInputの値を取得

        rigd.velocity = new Vector3(x * speed, rigd.velocity.y, z * speed); //プレイヤーのRigidbodyに対してInputにspeedを掛けた値で更新し移動

        Player_pos = transform.position; //プレイヤーの位置を更新
    }
}
