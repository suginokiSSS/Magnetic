using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Select : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Ø‚è‘Ö‚¦Œã‚ÌƒJƒƒ‰")]
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    [Tooltip("Ø‚è‘Ö‚¦‘O‚ÌƒJƒƒ‰")]
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
