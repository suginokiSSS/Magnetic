using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_groundcheck : MonoBehaviour
{
    player_jump playerscript;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Player");
        playerscript = obj.GetComponent<player_jump>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        playerscript._isGrounded = true;
    }
}
