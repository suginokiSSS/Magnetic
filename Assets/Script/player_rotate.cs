using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class player_rotate : MonoBehaviour
{
    private Quaternion previousRotation;
    private float rotationThreshold = 0.1f; // 回転判定の閾値（微小な回転を無視するための値）
    [HideInInspector] public float rightRotationValue = 0f; // 右回転時の値
    [HideInInspector] public float leftRotationValue = 0f; // 左回転時の値
    private float stopRotationValue = 0f; //止まっているときの値
    private float time = 0f; //時間

    public bool autoRotation = false;

    private Vector3 Player_pos; //プレイヤーのポジション

    [SerializeField]
    [Tooltip("y軸の回転角度")]
    private float rotateY = 450;

    player_catch playercatch;

    private void Start()
    {
        Player_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
        previousRotation = transform.rotation;

        playercatch = GetComponent<player_catch>();
    }

    private void FixedUpdate()
    {
        Vector3 diff = transform.position - Player_pos; //プレイヤーがどの方向に進んでいるかがわかるように、初期位置と現在地の座標差分を取得

        if (diff.magnitude > 0.01f && autoRotation == false) //ベクトルの長さが0.01fより大きい場合にプレイヤーの向きを変える処理を入れる(0では入れないので）
        {
            Quaternion lookRotation = Quaternion.LookRotation(diff, Vector3.up);
            transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
        }

        Player_pos = transform.position; //プレイヤーの位置を更新

        Quaternion currentRotation = transform.rotation;
        float rotationDifference = Quaternion.Angle(previousRotation, currentRotation);

        // 回転が閾値以上である場合に回転方向を判別
        if (rotationDifference > rotationThreshold)
        {
            Vector3 previousForward = previousRotation * Vector3.forward;
            Vector3 currentForward = currentRotation * Vector3.forward;
            Vector3 crossProduct = Vector3.Cross(previousForward, currentForward);

            // ベクトルの外積を利用して回転方向を判別
            if (crossProduct.y > 0)
            {
                Debug.Log("右回転値: " + rightRotationValue);
                rightRotationValue += Time.deltaTime * 50;
                stopRotationValue = 0;
                // 右回転時の処理をここに追加
            }
            else if (crossProduct.y < 0)
            {
                Debug.Log("左回転値: " + leftRotationValue);
                leftRotationValue += Time.deltaTime * 50;
                stopRotationValue = 0;
                // 左回転時の処理をここに追加
            }
        }
        else
        {
            Debug.Log("静止値" + stopRotationValue);
            stopRotationValue++;
            // 静止時の処理をここに追加
        }

        if (rightRotationValue >= 85f && time < 7)
        {
            // Y軸に対して、指定した角度ずつ回転させている。
            gameObject.transform.Rotate(new Vector3(0, rotateY, 0) * Time.deltaTime / (playercatch.Checkthrow / 5 + 1));
            time += Time.deltaTime;
            leftRotationValue = 0;
            autoRotation = true;
        }
        else if (leftRotationValue >= 85f && time < 7)
        {
            // Y軸に対して、指定した角度ずつ回転させている。
            gameObject.transform.Rotate(new Vector3(0, -rotateY, 0) * Time.deltaTime / (playercatch.Checkthrow / 5 + 1));
            time += Time.deltaTime;
            rightRotationValue = 0;
            autoRotation = true;
        }
        else if (rightRotationValue < 10 && leftRotationValue < 10)
        {
            time = 0;
            autoRotation = false;
        }

        if(rightRotationValue > 10)
        {
            leftRotationValue = 0;
        }
        else if (leftRotationValue > 10)
        {
            rightRotationValue = 0;
        }

        if (time == 7 || Input.GetKey("joystick button 0") || Input.GetKey(KeyCode.K))
        {
            time = 8;
            rightRotationValue = 0;
            leftRotationValue = 0;
            autoRotation = false;
        }

        if (stopRotationValue > 10)
        {
            rightRotationValue = 0;
            leftRotationValue = 0;
        }

        previousRotation = currentRotation;
    }
}
