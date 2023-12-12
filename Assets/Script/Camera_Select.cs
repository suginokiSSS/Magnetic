using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Select : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�؂�ւ���̃J����")]
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    [Tooltip("�؂�ւ��O�̃J����")]
    private CinemachineVirtualCamera beforevirtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            virtualCamera.Priority = 100;
            beforevirtualCamera.Priority = 10;
        }
    }
}
