using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monoSwitch : MonoBehaviour
{
    public GameObject[] mono;
    public Transform makePosition;

    private bool onplayer = false;

    public Transform targetPosition; // ������ʒu
    public float loweringSpeed = 5f; // �����鑬�x

    private Vector3 initialPosition; // ���̈ʒu
    private bool isLowering = false; // �������Ă��邩�ǂ����������t���O

    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(onplayer == true && !isLowering)
        {
            StartCoroutine(LowerObject());
        }
        else if(isLowering)
        {
            StartCoroutine(RaiseObject());
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            int rnd = Random.Range(1, 3);
            for (int i = 0; i < rnd; i++)
            {
                int rndmono = Random.Range(0, mono.Length);
                GameObject newMono = Instantiate(mono[rndmono], makePosition.transform.position, makePosition.transform.rotation);
            }
            soundManager.PlaySe(clip);
        }
    }

    IEnumerator LowerObject()
    {
        isLowering = true; // �������Ă��邱�Ƃ������t���O�𗧂Ă�

        while (Vector3.Distance(transform.position, targetPosition.position) > 0.05f)
        {
            // ������ʒu�Ɍ������ď��X�Ɉړ�����
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, loweringSpeed * Time.deltaTime);
            yield return null; // ���̃t���[���܂őҋ@
        }
    }

    IEnumerator RaiseObject()
    {
        while (Vector3.Distance(transform.position, initialPosition) > 0.05f)
        {
            // ���̈ʒu�ɖ߂�
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, loweringSpeed * Time.deltaTime);
            yield return null; // ���̃t���[���܂őҋ@
        }

        isLowering = false; // �������Ԃ���������
    }
}
