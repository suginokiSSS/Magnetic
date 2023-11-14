using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pouse : MonoBehaviour
{
    //　ポーズした時に表示するUI
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
            //　ポーズUIのアクティブ、非アクティブを切り替え
            pauseUI.SetActive(!pauseUI.activeSelf);

            //　ポーズUIが表示されてる時は停止
            if (pauseUI.activeSelf)
            {
                Time.timeScale = 0f;
                //　ポーズUIが表示されてなければ通常通り進行
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
            //　ポーズUIのアクティブ、非アクティブを切り替え
            pauseUI.SetActive(!pauseUI.activeSelf);

            mainUI.SetActive(!mainUI.activeSelf);
        }
    }
}
