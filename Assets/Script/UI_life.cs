using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_life : MonoBehaviour
{
    private int maxHearts = 3; // 最大ハート数
    public Image heartImagePrefab; // ハートの画像プレハブ
    public Transform heartContainer; // ハートの親オブジェクト

    //　やられた時に表示するUI
    [SerializeField]
    private GameObject gameoverUI;

    private Image[] hearts; // ハートの配列
    private int currentHearts; // 現在のハート数

    player_life lifescript;

    private void Start()
    {
        lifescript = GetComponent<player_life>();
        maxHearts = lifescript.life;
        hearts = new Image[maxHearts]; // ハートの配列を初期化
        currentHearts = maxHearts; // 現在のハート数を最大ハート数に設定

        // ハートの数だけハート画像を生成して配列に格納
        for (int i = 0; i < maxHearts; i++)
        {
            Image heart = Instantiate(heartImagePrefab, heartContainer);
            hearts[i] = heart;
        }
    }

    void Update()
    {
        if(lifescript.life < maxHearts)
        {
            DecreaseLife();
            maxHearts = lifescript.life;
        }

        if (lifescript.life <= 0 && !gameoverUI.activeSelf)
        {
            //　ゲームオーバーUIのアクティブ、非アクティブを切り替え
            gameoverUI.SetActive(!gameoverUI.activeSelf);
        }
    }

    // ライフが減った時に呼び出す関数
    public void DecreaseLife()
    {
        if (currentHearts > 0)
        {
            currentHearts--; // 現在のハート数を減らす
            hearts[currentHearts].enabled = false; // 最後のハートを非表示にする
        }
    }

    // ライフが増えた時に呼び出す関数
    public void IncreaseLife()
    {
        if (currentHearts < maxHearts)
        {
            hearts[currentHearts].enabled = true; // 次のハートを表示する
            currentHearts++; // 現在のハート数を増やす
        }
    }
}
