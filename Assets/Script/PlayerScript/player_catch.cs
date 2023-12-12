using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_catch : MonoBehaviour
{
    public float throwForce = 15f; 

    public float grabDistance = 5f;

    public float randomAngle = 45f;

    public bool Deathmono = true;

    [HideInInspector] public Rigidbody[] objectToThrow;

    [HideInInspector] public int Checkthrow = 0;

    player_rotate playerrotate;

    SoundManager soundManager;
    [SerializeField]
    AudioClip clip_catch;
    [SerializeField]
    AudioClip clip_throw;

    void Start()
    {
        playerrotate = GetComponent<player_rotate>();
        objectToThrow = new Rigidbody[200];
        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
    }

    void FixedUpdate()
    {
        if (Checkthrow == 0)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, grabDistance);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "throw")
                {
                    objectToThrow[Checkthrow] = collider.attachedRigidbody;
                    objectToThrow[Checkthrow].GetComponent<Rigidbody>().isKinematic = true;
                    objectToThrow[Checkthrow].GetComponent<Collider>().isTrigger = true;
                    objectToThrow[Checkthrow].gameObject.transform.parent = this.gameObject.transform;
                    Checkthrow++;
                    collider.tag = "catch";
                 
                    soundManager.PlaySe(clip_catch);
                    return;
                }
            }
        }

        if (objectToThrow[49] == null && Checkthrow > 0)
        {
            for (int i = 0; i < Checkthrow; i++)
            {
                Collider[] colliders = Physics.OverlapSphere(objectToThrow[i].transform.position, objectToThrow[i].transform.localScale.x * grabDistance);
                foreach (Collider collider in colliders)
                {
                    if (collider.tag == "throw")
                    {
                        objectToThrow[Checkthrow] = collider.attachedRigidbody;
                        objectToThrow[Checkthrow].GetComponent<Rigidbody>().isKinematic = true;
                        objectToThrow[Checkthrow].gameObject.transform.parent = this.gameObject.transform;
                        objectToThrow[Checkthrow].GetComponent<Collider>().isTrigger = true;
                        Checkthrow++;
                        collider.tag = "catch";
                        soundManager.PlaySe(clip_catch);
                        return;
                    }
                }
            }
        }

            for (int i = 0; i < Checkthrow; i++)
        {
            if (objectToThrow[i].tag == "stop")
            {
                for (int j = i; j < Checkthrow; j++)
                {
                    GameObject newMono = Instantiate(objectToThrow[j].gameObject, objectToThrow[j].position, objectToThrow[j].rotation);
                    Vector3 startthrowDirection = (this.transform.position - objectToThrow[j].position);
                    newMono.GetComponent<Rigidbody>().AddForce(startthrowDirection * 3, ForceMode.Force);
                    Destroy(objectToThrow[j].gameObject);
                }
                for (int j = Checkthrow; j > i; j--)
                {
                    Checkthrow--;
                    objectToThrow[j] = null;
                }
                return;
            }
        }

        if (playerrotate.autoRotation == true)
        {
            if (Input.GetKey("joystick button 2") || Input.GetKey(KeyCode.K))
            {              
                for (int i = 0; i < Checkthrow; i++)
                {
                    objectToThrow[i].gameObject.transform.parent = null;
                    objectToThrow[i].GetComponent<Rigidbody>().isKinematic = false;                 
                    objectToThrow[i].GetComponent<Collider>().isTrigger = false;
                    objectToThrow[i].tag = "fly";
                    objectToThrow[i].AddForce(transform.forward * throwForce, ForceMode.Impulse);

                    if (Deathmono == true)
                    {
                        Destroy(objectToThrow[i].gameObject, 1f);
                    }
                }
                Checkthrow = 0;
                soundManager.PlaySe(clip_throw);
                playerrotate.autoRotation = false;
                return;             
            }
        }

        if(playerrotate.autoRotation == false || Input.GetKey(KeyCode.O) || Input.GetKeyDown("joystick button 3"))
        {
            for (int i = 0; i < Checkthrow; i++)
            {
                GameObject newMono = Instantiate(objectToThrow[i].gameObject, objectToThrow[i].position, objectToThrow[i].rotation);
                Vector3 startthrowDirection = (this.transform.position - objectToThrow[i].position);
                startthrowDirection.y = 0f;
                newMono.GetComponent<Rigidbody>().AddForce(startthrowDirection * 3, ForceMode.Force);
                Destroy(objectToThrow[i].gameObject);
            }
            Checkthrow = 0;
            playerrotate.autoRotation = false;
            return;
        }       

        if (objectToThrow[0])
        {
            Vector3 throwDirection = this.transform.forward;
            throwDirection.y = 0.1f;
            objectToThrow[0].transform.position = this.transform.position + throwDirection * (objectToThrow[0].transform.localScale.x + 1);
        }
        else
        {
            playerrotate.autoRotation = false;
        }
    }
}
