using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("Stats")]
    public bool gameEnded = false;
    public float timeToWIn;
    public float invincibleDuration;
    private FlagsAttribute hatPickupTime;

    [Header("Players")]
    public string playerPrefabLocation;
    public Transform[] spawnPoints;
    public int playerWithHat;
    public int playersInGame;

    public static GameManager instance;
    internal object player;
    internal object players;

    void Awake()
    {
        instance = this;
    }

    [PunRPC]
    void ImINGame ()
    {
        playersInGame++;

        if (playersInGame == PhotonNetwork.PlayerList.Length)
            SpawnPlayer();
    }

    void SpawnPlayer ()
    {
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefabLocation, spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)], Quaternion.identity);

        PlayerController playerScript = playerObj.GetComponent<PlayerController>();

        playerScript.photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

    public PlayerController GetPlayer(int playerID)
    {
        return players.First(x => x.gameObject == playerObject);

    }

    public PlayerController GetPlayer (GameObject playerObject)
    {
        return players.First(x => x.gameObject == playerObject);
    }


    [PunRPC]
    public void GiveHat (int playerID, bool initialGive)
    {
        if (!initialGive)
            GetPlayer(playerWithHat).SetHat(false);

        playerWithHat = playerID;
        GetPlayer(playerID).SetHat(true);
        hatPickupTime = Time.time;
    }

    public bool CanGetHat ()
    {
        if (Time.time > hatPickupTime + invincibleDuration)
            return true;
        else 
            return false;
    }

    [PunRPC]
    void WinGame (int playerID)
    {
        gameEnded = true;
        PlayerController player = GetPlayer(playerID);

        Invoke("GoBackToMenu", 3.0f);

        GameUI.instance.SetWinText(player.photonPlayer.NickName);
    }

    void GoBackToMenu ()
    {
        PhotonNetwork.LeaveRoom();
        NetworkManager.instance.ChangeScene("Menu");
    }

    public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(curHatTime);
        }
        else if (stream.IsReading)
        {
            curhatTime = (float)stream.ReceiveNext();
        }
    }
}

