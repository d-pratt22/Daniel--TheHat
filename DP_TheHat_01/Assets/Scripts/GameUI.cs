using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class GameUI : MonoBehaviour
{
    [System.Serializable]
    public class PlayerUIContainer
    {
        public GameObject obj;
        public TextMeshProUGUI nameText;
        public Slider hatTimeSlider;
    }

    public PlayerController[] playerContainers;
    public TextMeshProUGUI winText;

    public static GameUI instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitializePlayerUI();
    }

    void InitializePlayerUI()
    {
        for(int x = 0; x < playerContainers.Length; ++x)
        {
            PlayerUIContainer container = playerContainers[x];

            if(x<PhotonNetwork.PlayerList.Length)
            {
                container.obj.SetActive(true);
                container.nameText.text = PhotonNetwork.PlayerList[x].NickName;
                container.hatTimeSlider.maxValue = GameManager.instance.timeToWIn;
            }
            else
                container.obj.SetActive(false);
        }
    }

    void Update ()
    {
        UpdatePlayerUI();
    }

    void UpdatePlayerUI ()
    {
        for (int x= 0; x<GameManager.instance.players.Length;  ++x)
        {
            if (GameManager.instance.players != null)
                playerContainers[x].hatTimeSlider.value = GameManager.instance.players.curHatTime;
        }
    }

   public void SetWinText (string winnerName)
    {
        winText.gameObject.SetActive(true);
        winText.text = winnerName + " wins";
    }
}
