using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_catch : MonoBehaviour
{
    public float throwForce = 15f; 

    public float grabDistance = 5f;

    public float randomAngle = 45f;

    private Rigidbody[] objectToThrow;

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
        objectToThrow = new Rigidbody[50];
        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E) || Input.GetKey("joystick button 4"))
        {
            if (playerrotate.autoRotation == true)
            {
                Time.timeScale = 0.5f;
            }        
        }
        else
        {
            Time.timeScale = 1.0f;
        }

        if (Checkthrow == 0)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, grabDistance);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "throw")
                {
                    objectToThrow[Checkthrow] = collider.attachedRigidbody;
                    objectToThrow[Checkthrow].GetComponent<Rigidbody>().isKinematic = true;
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
                Collider[] colliders = Physics.OverlapSphere(objectToThrow[i].transform.position, grabDistance);
                foreach (Collider collider in colliders)
                {
                    if (collider.tag == "throw")
                    {
                        objectToThrow[Checkthrow] = collider.attachedRigidbody;
                        objectToThrow[Checkthrow].GetComponent<Rigidbody>().isKinematic = true;
                        objectToThrow[Checkthrow].gameObject.transform.parent = this.gameObject.transform;
                        Checkthrow++;
                        collider.tag = "catch";
                        playerrotate.Maxrotate += 50f;
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
                    startthrowDirection.y = 0f;
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

        if(playerrotate.autoRotation == false || Input.GetKey(KeyCode.O))
        {
            for (int i = 0; i < Checkthrow; i++)
            {
                GameObject newMono = Instantiate(objectToThrow[i].gameObject, objectToThrow[i].position, objectToThrow[i].rotation);
                Vector3 startthrowDirection = (this.transform.position - objectToThrow[i].position);
                startthrowDirection.y = 0f;
                newMono.GetComponent<Rigidbody>().AddForce(startthrowDirection * 3, ForceMode.Force);
                Destroy(objectToThrow[i].gameObject);
            }
            for (int i = Checkthrow; i > 0; i--)
            {
                Checkthrow--;
                objectToThrow[i] = null;
            }
            return;
        }

        if (playerrotate.autoRotation == true && objectToThrow[0] != null)
        {
            if (Input.GetKey("joystick button 0") || Input.GetKey(KeyCode.K))
            {              
                for (int i = 0; i < Checkthrow; i++)
                {
                    // ƒ‰ƒ“ƒ_ƒ€‚ÈŠp“x‚ð¶¬‚µA‚»‚ê‚ÉŠî‚Ã‚¢‚Ä•ûŒü‚ðŒvŽZ
                    float randomYaw = Random.Range(-randomAngle, randomAngle);
                    float randomPitch = Random.Range(-randomAngle, randomAngle);
                    Quaternion randomRotation = Quaternion.Euler(randomPitch, randomYaw, 0f);

                    objectToThrow[i].GetComponent<Rigidbody>().isKinematic = false;
                    objectToThrow[i].gameObject.transform.parent = null;
                    Vector3 throwDirection = transform.forward;
                    throwDirection.y = 0f;
                    objectToThrow[i].tag = "fly";
                    objectToThrow[i].GetComponent<Rigidbody>().AddForce(throwDirection * throwForce, ForceMode.Impulse);
                    Destroy(objectToThrow[i].gameObject, 1f);
                }
                for (int i = Checkthrow; i > 0; i--)
                {
                    Checkthrow--;
                    objectToThrow[i] = null;
                }
                soundManager.PlaySe(clip_throw);
                return;             
            }

        }
        if (objectToThrow[0])
        {
            Vector3 throwDirection = this.transform.forward;
            throwDirection.y = 0f;
            objectToThrow[0].transform.position = this.transform.position + throwDirection * (1 + 1);
        }
    }
}
