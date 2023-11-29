using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMove : MonoBehaviour
{
    public float targetRotation = 90f; // ñ⁄ïWÇÃâÒì]äpìx
    public float rotationSpeed = 50f; // âÒì]ë¨ìx

    private float currentRotation = 0f; // åªç›ÇÃâÒì]äpìx

    private bool targetplus = true;

    public bool dontMove = false;

    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
        if (targetRotation < 0)
        {
            targetplus = false;
            rotationSpeed = rotationSpeed * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetplus == true)
        {
            // è’ìÀÇµÇƒÇ¢Ç»Ç¢èÍçáÇÕñ⁄ïWÇÃâÒì]äpìxÇ…ìûíBÇ∑ÇÈÇ‹Ç≈âÒì]Ç≥ÇπÇÈ
            if (this.tag == "onswitch" && currentRotation < targetRotation)
            {
                float rotationAmount = rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationAmount);
                currentRotation += rotationAmount;
            }
        }
        else
        {
            // è’ìÀÇµÇƒÇ¢Ç»Ç¢èÍçáÇÕñ⁄ïWÇÃâÒì]äpìxÇ…ìûíBÇ∑ÇÈÇ‹Ç≈âÒì]Ç≥ÇπÇÈ
            if (this.tag == "onswitch" && currentRotation > targetRotation)
            {
                float rotationAmount = rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationAmount);
                currentRotation += rotationAmount;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.tag != "onswitch" && dontMove == false)
        {
            if (other.gameObject.CompareTag("catch") || other.gameObject.CompareTag("fly"))
            {
                this.tag = "onswitch";

                soundManager.PlaySe(clip);
            }
        }
    }
}
