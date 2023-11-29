using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bomb : MonoBehaviour
{
    public GameObject bombObject;

    enemy_move enemyscript;
    // Start is called before the first frame update
    void Start()
    {
        enemyscript = GetComponent<enemy_move>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyscript.isStunned == true)
        {
            Invoke("Bomb", 1.0f);
        }
    }

    void Bomb()
    {
        //enemy���C���X�^���X������(��������)
        GameObject bakuhatu = Instantiate(bombObject);
        //���������G�̍��W�����肷��
        bakuhatu.transform.position = this.transform.position;
        //���S
        Destroy(bakuhatu, 0.5f);
        Destroy(this.gameObject);
    }
}
