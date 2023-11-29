using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_animation : MonoBehaviour
{
    private Animator animator;

    enemy_move enemyscript;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = transform.parent.gameObject;
        enemyscript = obj.GetComponent<enemy_move>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyscript.canmove == true)
        {
            animator.SetBool("Stunned", true);
        }
        else
        {
            animator.SetBool("Stunned", false);
        }
    }
}
