using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviourPunCallbacks
{
    public GameObject mainMenu;
    public GameObject loadingMenu;
    public static Controller instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Connecting...");
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        loadingMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void PlayGame()
    {
        //if (string.IsNullOrEmpty(playerNickname.text))
        //{
        //    Debug.Log("Nickname is Empty");
        //    nicknameEmpty.SetActive(true);
        //    return;
        //}
        //else
        //{
        //    nicknameEmpty.SetActive(false);
        //}

        //PhotonNetwork.NickName = playerNickname.text;
        PhotonNetwork.JoinRandomRoom();

        //MenuManager.instance.OpenMenu("Waiting");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Room not Found!");
        CreateNewRoom();
    }

    void CreateNewRoom()
    {
        int randomNumber = Random.Range(0, 1000);
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2
        };

        PhotonNetwork.CreateRoom("Room" + randomNumber, roomOptions);
        Debug.Log("New Room Created!");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            mainMenu.SetActive(false);
            loadingMenu.SetActive(false);
            //MenuManager.instance.OpenMenu("Loading");
            PhotonNetwork.LoadLevel(2);
        }
    }
}
