using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    SoundManager soundManager;
    [SerializeField]
    AudioClip clip_goal;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            soundManager.PlaySe(clip_goal);
            Invoke("ChangeScene", 0.3f);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("Result");
    }
}
