using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monoSwitch : MonoBehaviour
{
    public GameObject[] mono;
    public Transform makePosition;

    private bool onplayer = false;

    public Transform targetPosition; // 下がる位置
    public float loweringSpeed = 5f; // 下がる速度

    private Vector3 initialPosition; // 元の位置
    private bool isLowering = false; // 下がっているかどうかを示すフラグ

    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(onplayer == true && !isLowering)
        {
            StartCoroutine(LowerObject());
        }
        else if(isLowering)
        {
            StartCoroutine(RaiseObject());
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            int rnd = Random.Range(1, 3);
            for (int i = 0; i < rnd; i++)
            {
                int rndmono = Random.Range(0, mono.Length);
                GameObject newMono = Instantiate(mono[rndmono], makePosition.transform.position, makePosition.transform.rotation);
            }
            soundManager.PlaySe(clip);
        }
    }

    IEnumerator LowerObject()
    {
        isLowering = true; // 下がっていることを示すフラグを立てる

        while (Vector3.Distance(transform.position, targetPosition.position) > 0.05f)
        {
            // 下がる位置に向かって徐々に移動する
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, loweringSpeed * Time.deltaTime);
            yield return null; // 次のフレームまで待機
        }
    }

    IEnumerator RaiseObject()
    {
        while (Vector3.Distance(transform.position, initialPosition) > 0.05f)
        {
            // 元の位置に戻る
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, loweringSpeed * Time.deltaTime);
            yield return null; // 次のフレームまで待機
        }

        isLowering = false; // 下がり状態を解除する
    }
}
