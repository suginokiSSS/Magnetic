using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_life : MonoBehaviour
{
    [SerializeField]
    public int life = 3;

    private float time;

    public GameObject breakEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (life == 0)
        {
            Invoke("Activefalse", 0.2f);
            //�G�t�F�N�g�𔭐�������
            GenerateEffect();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && time > 2)
        {
            life--;
            time = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy" && time > 2)
        {
            life--;
            time = 0f;
        }
    }

    //�G�t�F�N�g�𐶐�����
    void GenerateEffect()
    {
        //�G�t�F�N�g�𐶐�����
        GameObject effect = Instantiate(breakEffect) as GameObject;
        //�G�t�F�N�g����������ꏊ�����肷��
        effect.transform.position = gameObject.transform.position;
    }

    void Activefalse()
    {
        this.gameObject.SetActive(false);
    }
}
