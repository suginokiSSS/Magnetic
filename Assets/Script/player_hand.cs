using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_hand : MonoBehaviour
{
    public Transform object1; // 回転させるオブジェクト1
    public Transform object2; // 回転させるオブジェクト2
    public float rotationSpeed = 1.0f; // 回転速度

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Rボタンが押されたら
        {
            RotateObjectsAlongYAxis(); // Y軸周りに回転する関数を呼び出す
        }
    }

    void RotateObjectsAlongYAxis()
    {
        object1.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        object2.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
