using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pouse : MonoBehaviour
{
    //�@�|�[�Y�������ɕ\������UI
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private GameObject mainUI;

    public bool action = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p") && action == true)
        {
            //�@�|�[�YUI�̃A�N�e�B�u�A��A�N�e�B�u��؂�ւ�
            pauseUI.SetActive(!pauseUI.activeSelf);

            //�@�|�[�YUI���\������Ă鎞�͒�~
            if (pauseUI.activeSelf)
            {
                Time.timeScale = 0f;
                //�@�|�[�YUI���\������ĂȂ���Βʏ�ʂ�i�s
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void OnClick()
    {
        if (action == false)
        {
            //�@�|�[�YUI�̃A�N�e�B�u�A��A�N�e�B�u��؂�ւ�
            pauseUI.SetActive(!pauseUI.activeSelf);

            mainUI.SetActive(!mainUI.activeSelf);
        }
    }
}
