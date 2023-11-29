using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_tower : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public GameObject enemyPrefab4; 
    public GameObject enemyPrefab5;

    public Transform step1;
    public Transform step2;
    public Transform step3;
    public Transform step4;
    public Transform step5;

    private bool isEnemy1 = false;
    private bool isEnemy2 = false;
    private bool isEnemy3 = false;
    private bool isEnemy4 = false;
    private bool isEnemy5 = false;

    // Start is called before the first frame update
    void Start()
    {
        if (enemyPrefab1)
        {
            isEnemy1 = true;
        }

        if (enemyPrefab2)
        {
            isEnemy2 = true;
            enemyPrefab2.GetComponent<enemy_move>().enabled = false;
        }

        if (enemyPrefab3)
        {
            isEnemy3 = true;
            enemyPrefab3.GetComponent<enemy_move>().enabled = false;
        }

        if (enemyPrefab4)
        {
            isEnemy4 = true;
            enemyPrefab4.GetComponent<enemy_move>().enabled = false;
        }

        if (enemyPrefab5)
        {
            isEnemy5 = true;
            enemyPrefab5.GetComponent<enemy_move>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemy1)
        {
            enemyPrefab1.transform.position = step1.position;
            enemyPrefab1.transform.rotation = step1.rotation;
        }

        if (isEnemy2)
        {
            enemyPrefab2.transform.position = step2.position;
            enemyPrefab2.transform.rotation = step2.rotation;
        }

        if (isEnemy3)
        {
            enemyPrefab3.transform.position = step3.position;
            enemyPrefab3.transform.rotation = step3.rotation;
        }

        if (isEnemy4)
        {
            enemyPrefab4.transform.position = step4.position;
            enemyPrefab4.transform.rotation = step4.rotation;
        }

        if (isEnemy5)
        {
            enemyPrefab5.transform.position = step5.position;
            enemyPrefab5.transform.rotation = step5.rotation;
        }
    }
}
