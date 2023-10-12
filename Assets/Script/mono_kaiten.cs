using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mono_kaiten : MonoBehaviour
{
    private float nowYPosi;
    private float bound = 3f;
    private float xmove;
    private float zmove;

    // Start is called before the first frame update
    void Start()
    {
        xmove = Random.Range(-0.05f, 0.05f);
        zmove = Random.Range(-0.05f, 0.05f);
        Invoke("ChangeTag", 1.5f);
        nowYPosi = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (bound > 0)
        {
            bound -= 0.05f;
            transform.position = new Vector3(this.transform.position.x + xmove, nowYPosi + Mathf.PingPong(Time.time, bound), this.transform.position.z + zmove);
        }

        transform.Rotate(new Vector3(0, 30f, 0) * Time.deltaTime);
    }

    void ChangeTag()
    {
        this.tag = "throw";
    }
}
