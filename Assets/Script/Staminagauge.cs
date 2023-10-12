using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staminagauge : MonoBehaviour
{
    private float RotationValue = 0f;
    private float time;

    public Image gaugeImage;

    player_rotate playerscript;
    

    // Start is called before the first frame update
    void Start()
    {
        gaugeImage = GetComponent<Image>();
        GameObject obj = GameObject.Find("Player");
        playerscript = obj.GetComponent<player_rotate>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotationValue = playerscript.rightRotationValue + playerscript.leftRotationValue;

        if (RotationValue < 90f)
        {
            gaugeImage.color = new Color(1f, 127f / 255f, 39f / 255f);
            time = 7;
            gaugeImage.fillAmount = RotationValue / 90;
        }
        else
        {
            gaugeImage.color = Color.green;
            time -= Time.deltaTime;
            gaugeImage.fillAmount = time / 7;
        }
    }
}
