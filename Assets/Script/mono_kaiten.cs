using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mono_kaiten : MonoBehaviour
{
    private float nowYPosi;
    private float xmove;
    private float zmove;
    private float bound = 3f;
    public bool once = false;

    public GameObject maguEffect;

    // Start is called before the first frame update
    void Start()
    {
        xmove = Random.Range(-0.2f, 0.2f);
        zmove = Random.Range(-0.2f, 0.2f);
        Invoke("ChangeTag", 1f);
        nowYPosi = this.transform.position.y;
    }
    
    // Update is called once per frame
    void Update()
    {

        if (bound > 0 && once == false)
        {
            bound -= 0.2f;
            transform.position = new Vector3(this.transform.position.x + xmove, nowYPosi + Mathf.PingPong(Time.time, bound), this.transform.position.z + zmove);
        }

        if (once == false && this.tag == "catch")
        {
            //エフェクトを生成する
            GameObject effect = Instantiate(maguEffect) as GameObject;
            //エフェクトが発生する場所を決定する
            effect.transform.position = gameObject.transform.position;
        }
    }

    void ChangeTag()
    {
        this.tag = "throw";
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            if(this.tag == "fly")
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            if (this.tag == "catch")
            {
                this.tag = "stop";
            }
        }
    }
}
