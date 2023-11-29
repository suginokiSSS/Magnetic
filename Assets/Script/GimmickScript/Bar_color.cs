using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar_color : MonoBehaviour
{
    public GameObject[] objectsToColor; // �F��ς������I�u�W�F�N�g�̔z��
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in objectsToColor)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                // �I�u�W�F�N�g�̐F��ύX
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
                    // �I�u�W�F�N�g�̐F��ύX
                    renderer.material.color = Color.green;
                }
            }
        }
    }
}
