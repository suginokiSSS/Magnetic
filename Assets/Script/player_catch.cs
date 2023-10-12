using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_catch : MonoBehaviour
{
    public float throwForce = 15f;  // 投げる力

    public float grabDistance = 5f; // 掴む距離

    private Rigidbody[] objectToThrow; // 投げるオブジェクトのRigidbody

    public int Checkthrow = 0;

    [SerializeField]
    [Tooltip("弾1")]
    private GameObject bullet1;

    player_rotate playerrotate;

    void Start()
    {
        playerrotate = GetComponent<player_rotate>();
        objectToThrow = new Rigidbody[30];
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E) && playerrotate.autoRotation == true)
        {
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }

        //手の位置から掴む距離以内にあるtagが"throw"のオブジェクトを取得
        if (objectToThrow.Length == 0)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, grabDistance);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "throw")
                {
                    objectToThrow[Checkthrow] = collider.attachedRigidbody;
                    Checkthrow++;
                    collider.tag = "catch";
                    return;
                }
            }
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, grabDistance);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "throw")
                {
                    objectToThrow[Checkthrow] = collider.attachedRigidbody;
                    Checkthrow++;
                    collider.tag = "catch";
                    return;
                }
            }
        }

        if (playerrotate.autoRotation == true)
        {
            // Kボタン押すとオブジェクトを投げる
            if (Input.GetKey("joystick button 0") || Input.GetKey(KeyCode.K))
            {
                if (objectToThrow[0] != null)
                {
                    GameObject newBullet = Instantiate(bullet1, this.transform.position, transform.rotation);
                    // 投げる方向を計算
                    Vector3 throwDirection = transform.forward;
                    throwDirection.y = 0f;
                    // 手の位置を基準に、オブジェクトを投げる
                    newBullet.GetComponent<Rigidbody>().AddForce(throwDirection * throwForce, ForceMode.Impulse);

                    // 弾が生成された瞬間のスケールを保存
                    Vector3 initialScale = newBullet.transform.localScale;

                    // 弾が生成されてからの経過時間を計測
                    float elapsedTime = 0f;

                    // 弾が生成されてから3秒間は大きくなる
                    while (elapsedTime < 3f)
                    {
                        // 時間経過を加算
                        elapsedTime += Time.deltaTime;

                        // 弾のスケールを変更して大きくする
                        newBullet.transform.localScale = initialScale * (1 + Checkthrow);
                    }

                        Destroy(newBullet, 1f);
                }

                if (Checkthrow >= 1)
                {
                    for (int i = 0; i < Checkthrow; i++)
                    {
                        Destroy(objectToThrow[i].gameObject);
                        objectToThrow[i] = null;
                    }
                    Checkthrow = 0;
                }
            }

        }

        // オブジェクトをつかんでいる間は、手の位置に合わせる
        for (int i = 0; i < Checkthrow; i++)
        {
            if (objectToThrow[i])
            {
                Vector3 throwDirection = this.transform.forward;
                throwDirection.y = 0f;
                objectToThrow[i].transform.position = this.transform.position + throwDirection * (i + 1) * 2;
            }
        }
    }
}
