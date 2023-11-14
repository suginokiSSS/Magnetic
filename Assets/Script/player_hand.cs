using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_hand : MonoBehaviour
{
    public Transform object1; // ��]������I�u�W�F�N�g1
    public Transform object2; // ��]������I�u�W�F�N�g2
    public float rotationSpeed = 1.0f; // ��]���x

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // R�{�^���������ꂽ��
        {
            RotateObjectsAlongYAxis(); // Y������ɉ�]����֐����Ăяo��
        }
    }

    void RotateObjectsAlongYAxis()
    {
        object1.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        object2.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
