using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class LavaController : MonoBehaviourPunCallbacks
{
    [Header("Info")]
    public float moveSpeed;

   [Header("Components")]
    public Rigidbody rig;
    public Transform[] hatSpawn;


    public GameObject Hat;

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);

    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.name);
        if(collider.gameObject.tag == "Player")
        {
            Debug.Log("you have been destroyed??");
            Destroy(collider.gameObject);
            
            if(Hat == isActiveAndEnabled)
            {
                GameObject playerObj = PhotonNetwork.Instantiate(Hat, hatSpawn[Random.Range(0, hatSpawn.Length)].position, Quaternion.identity, 0);
            }
        }
    }
}


