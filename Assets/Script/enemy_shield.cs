using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_shield : MonoBehaviour
{
    enemy_backmove enemyscript;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = transform.parent.gameObject;
        enemyscript = obj.GetComponent<enemy_backmove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "fly")
        {
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "catch")
        {
            enemyscript.SendMessage("TakeDamage");
        }
    }
}
