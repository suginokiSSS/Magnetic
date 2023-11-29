using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_animation : MonoBehaviour
{
    player_catch catchscript;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Player");
        catchscript = obj.GetComponent<player_catch>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(catchscript.Checkthrow >= 1)
        {
            animator.SetBool("catch", true);
        }
        else
        {
            animator.SetBool("catch", false);
        }
    }
}
