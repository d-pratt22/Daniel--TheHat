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

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("you have been destroyed??");
            Destroy(gameObject);
            
        }
    }
}


