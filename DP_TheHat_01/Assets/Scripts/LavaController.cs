using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Linq;

public class LavaController : MonoBehaviourPunCallbacks
{
    [Header("Info")]
    public float moveSpeed;

   [Header("Components")]
    public Rigidbody rig;
    public Transform[] hatSpawn;
    public PlayerController[] players;

    public GameObject Hat;

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);

    }

    public PlayerController GetPlayer(int playerID)
    {
        return players.First(x => x.id == playerID);

    }

    [PunRPC]
    private void OnTriggerEnter(Collider collider, int playerID)
    {
        Debug.Log(collider.name);
        if(collider.gameObject.tag == "Player")
        {
            Debug.Log("you have been destroyed??");
            Destroy(collider.gameObject);
            GetPlayer(playerID).SetHat(true);    
            /*if(Hat == isActiveAndEnabled)
            {
                GameObject playerObj = PhotonNetwork.Instantiate(Hat, hatSpawn[Random.Range(0, hatSpawn.Length)].position, Quaternion.identity, 0);
            }*/
        }
    }
}


