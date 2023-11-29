using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar_color : MonoBehaviour
{
    public GameObject[] objectsToColor; // 色を変えたいオブジェクトの配列
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in objectsToColor)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                // オブジェクトの色を変更
                renderer.material.color = Color.red;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(this.tag == "onswitch")
        {
            foreach (GameObject obj in objectsToColor)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    // オブジェクトの色を変更
                    renderer.material.color = Color.green;
                }
            }
        }
    }
}
