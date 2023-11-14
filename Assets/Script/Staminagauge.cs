using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staminagauge : MonoBehaviour
{
    private float RotationValue = 0f;

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
        RotationValue = playerscript.Maxrotate;

        if (RotationValue < 500f)
        {
            gaugeImage.color = new Color(1f, 127f / 255f, 39f / 255f);
            gaugeImage.fillAmount = RotationValue / 500;
        }
    }
}
