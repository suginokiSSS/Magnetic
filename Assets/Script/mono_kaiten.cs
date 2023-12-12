using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mono_kaiten : MonoBehaviour
{
    private float nowYPosi;
    private float xmove;
    private float zmove;
    private float bound = 3f;
    private float time = 0f;
    public bool once = false;

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
        if(this.tag == "catch")
        {
            time += Time.deltaTime;
        }

        if (bound > 0)
        {
            bound -= 0.2f;
            transform.position = new Vector3(this.transform.position.x + xmove, nowYPosi + Mathf.PingPong(Time.time, bound), this.transform.position.z + zmove);
        }

        if(this.tag == "fly")
        {
            if(once == false)
            {
                time = 0f;
                once = true;
            }

            time += Time.deltaTime;

            if(time > 2f)
            {
                this.tag = "throw";
            }
        }
    }

    void ChangeTag()
    {
        this.tag = "throw";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            if (this.tag == "catch" && time >= 2f)
            {
                this.tag = "stop";
                time = 0f;
            }
        }
    }
}
