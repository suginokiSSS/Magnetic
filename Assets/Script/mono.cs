using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mono : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerObject = null;
    
    //����̑��x
    [SerializeField]
    private float _speed = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.gameObject.tag == "throw")
        {
            //�v���C���[�Ɍ������Đi�܂���
            transform.position = Vector3.MoveTowards(transform.position, _playerObject.transform.position, _speed);
        }
    }
}
