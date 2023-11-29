using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_rotate : MonoBehaviour
{
    private Quaternion previousRotation;
    [HideInInspector] public float rightRotationValue = 0f; // 右回転時の値
    [HideInInspector] public float leftRotationValue = 0f; // 左回転時の値
    [HideInInspector] public float Maxrotate = 0f;

    public bool autoRotation = true;

    private bool rightrotation = true;

    private Vector3 Player_pos; //プレイヤーのポジション

    player_catch playercatch;

    Open_up openscript;

    private void Start()
    {
        Player_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
        previousRotation = transform.rotation;

        playercatch = GetComponent<player_catch>();

        GameObject obj = GameObject.Find("Door");
        openscript = obj.GetComponent<Open_up>();
    }

    private void FixedUpdate()
    {
        //if (openscript.move == true && autoRotation == true)
        //{
        //    Maxrotate ++;
        //}

        //if (Maxrotate >= 601f)
        //{
        //    Maxrotate = 600f;
        //}

        Vector3 diff = transform.position - Player_pos; //プレイヤーがどの方向に進んでいるかがわかるように、初期位置と現在地の座標差分を取得

        if (diff.magnitude > 0.01f && autoRotation == false) //ベクトルの長さが0.01fより大きい場合にプレイヤーの向きを変える処理を入れる(0では入れないので）
        {
            Quaternion lookRotation = Quaternion.LookRotation(diff, Vector3.up);
            transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
            Player_pos = transform.position; //プレイヤーの位置を更新
        }

        Quaternion currentRotation = transform.rotation;
        float rotationDifference = Quaternion.Angle(previousRotation, currentRotation);
        previousRotation = currentRotation;

        if(playercatch.Checkthrow >= 1 && autoRotation == false)
        {
            autoRotation = true;
            //Maxrotate = 500f;
        }

        if (Input.GetKey(KeyCode.L) || Input.GetKey("joystick button 5"))
        {
            rightrotation = true;
            // y軸を中心に回転させる
            transform.Rotate(0, 300f * Time.deltaTime, 0);
            //Maxrotate -= 0.5f;
        }
        else if (Input.GetKey(KeyCode.J) || Input.GetKey("joystick button 4"))
        {
            rightrotation = false;
            // y軸を中心に回転させる
            transform.Rotate(0, -300f * Time.deltaTime, 0);
            //Maxrotate -= 0.5f;
        }

        if (playercatch.Checkthrow > 0)
        {
            if (rightrotation == true)
            {
                transform.Rotate(0, 200f * Time.deltaTime / ((float)playercatch.Checkthrow / 7 + 1), 0);
                //Maxrotate--;
            }
            else
            {
                // y軸を中心に回転させる
                transform.Rotate(0, -200f * Time.deltaTime / ((float)playercatch.Checkthrow / 7 + 1), 0);
                //Maxrotate--;
            }
        }

        //if (Maxrotate <= 0)
        //{
        //    autoRotation = false;
        //}

        if (Input.GetKey("joystick button 2") || Input.GetKey("joystick button 3") || Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.O))
        {
            Maxrotate = -100;
            autoRotation = false;
        }
    }
}
