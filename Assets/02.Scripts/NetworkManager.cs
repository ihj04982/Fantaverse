using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    #region UI Panel
    public enum ActivePanel
    {
        LOGIN = 0,
        CHAT = 1,
        AESPA = 2,
        BTS = 3,
        TWICE = 4
    }
    public ActivePanel activePanel = ActivePanel.LOGIN;
    public GameObject[] panels;
    #endregion

    public InputField nameInput;        // SignIn InputField
    //Chat Components
    public GameObject UI_Chat;
    public InputField chatInput;
    public Text[] chats;

    //Network Components
    public bool isReLobby = false;
    public byte maxplayer = 3;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom("Room2", new RoomOptions { MaxPlayers = 3 }, TypedLobby.Default);
    }

    // 서버 연결시 바로 포톤 로비로 연결 시킨다
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    // 포톤 로비에 연결 될 경우 방(이름 : 로비) 생성하여 들어감
    public override void OnJoinedLobby()
    {
        if(isReLobby == true)
        {
            PhotonNetwork.LoadLevel(1);
            PhotonNetwork.JoinRoom("Room1");
        }
        isReLobby = false;
    }
    public override void OnJoinedRoom()     // 어느 방이든 들어간 경우 
    {
        ChangePanel(ActivePanel.CHAT);      // Chat UI Activate
        Vector2 pos = Random.insideUnitCircle * 1.5f;       // Player Instantiate
        GameObject myPlayer = PhotonNetwork.Instantiate("Player", new Vector3(pos.x, 0.5f, pos.y), Quaternion.identity);
        myPlayer.transform.GetChild(0).gameObject.SetActive(true);      // Main Camera Activate
        myPlayer.transform.GetChild(1).gameObject.SetActive(true);      // Sub Camera Activate
        chatInput.text = "<color=#8191B5>Welcome " + PhotonNetwork.LocalPlayer.NickName + "!</color>";      // Player 입장 Chat
        photonView.RPC("ChatRPC", RpcTarget.All, "" + chatInput.text);
        chatInput.text = "";
    }

    // UI 변경
    public void ChangePanel(ActivePanel panel)
    {
        foreach (GameObject _panel in panels)
        {
            _panel.SetActive(false);
        }
        panels[(int)panel].SetActive(true);
    }

    #region Aespa Room 생성
    public void OnABlackMambaClick()      //  Aespa - Black Mamba
    {
        PhotonNetwork.JoinOrCreateRoom("Black Mambal", new RoomOptions { MaxPlayers = maxplayer }, TypedLobby.Default);
    }
    public void OnANextLevelClick()      //  Aespa - Next Level
    {
        PhotonNetwork.JoinOrCreateRoom("Next Level", new RoomOptions { MaxPlayers = maxplayer }, TypedLobby.Default);
        PhotonNetwork.LoadLevel(3);
    }
    public void OnASavageClick()      //  Aespa - Savage
    {
        
        PhotonNetwork.JoinOrCreateRoom("Savage", new RoomOptions { MaxPlayers = maxplayer }, TypedLobby.Default);
        PhotonNetwork.LoadLevel(4);
    }    
    #endregion

    #region BTS Room 생성
    public void OnBDNAClick()      //  BTS - DNA
    {
        PhotonNetwork.JoinOrCreateRoom("DNA", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
    }
    public void OnBYetToComeClick()      //  BTS - YetToCome
    {
        PhotonNetwork.JoinOrCreateRoom("Yet To Come", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
        PhotonNetwork.LoadLevel(6);
    }
    public void OnBButterClick()      //  BTS - Butter
    {
        PhotonNetwork.JoinOrCreateRoom("Butter", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
        PhotonNetwork.LoadLevel(5);
    }
    #endregion

    #region Twice Room 생성
    public void OnTDanceClick()      //  Twice - Dance Night Away
    {
        PhotonNetwork.JoinOrCreateRoom("Dance Night Away", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
     }
    public void OnTWhatClick()      //  Twice - What is Love
    {
        PhotonNetwork.JoinOrCreateRoom("What is Love", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
        PhotonNetwork.LoadLevel(9);
    }
    public void OnTScientistClick()      //  Twice - Scientist
    {
        PhotonNetwork.JoinOrCreateRoom("Scientist", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
        PhotonNetwork.LoadLevel(10);
    }
    #endregion

    #region Button Click 이벤트
    // SignIn 버튼 이벤트
    public void ConnectToLobby()
    {
        if (nameInput.text != "")      // 사용자가 입력한 유저네임을 넣는다
        {
            PhotonNetwork.LocalPlayer.NickName = nameInput.text;
        }
        else      // 입력한 유저네임이 없다면 유저네임 임의 생성
        {
            PhotonNetwork.LocalPlayer.NickName = "Fan" + Random.Range(1, 99);
        }
        PhotonNetwork.JoinOrCreateRoom("Room1", new RoomOptions { MaxPlayers = 3 }, TypedLobby.Default);
    }

    public void OnLobbyClick()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void ExitToLobby()    // 로비로 이동
    {
        PhotonNetwork.JoinRoom("Room1");
        PhotonNetwork.LoadLevel(1);
    }

    public void OnExitInChatClick()
    {
        isReLobby = true;
        PhotonNetwork.LeaveRoom();
    }

    public void OnQuitClick()
    {
        print("Quit");
        Application.Quit();
    }

    public void SendChat()
    {
        // 내가 입력한 글자를 보낸다 (방장이 처리)
        photonView.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + chatInput.text);
        chatInput.text = "";
    }
    [PunRPC]
    void ChatRPC(string chat)
    {
        bool isInput = false;       // 입력 여부
        for (int i = 0; i < chats.Length; i++)
        {
            if (chats[i].text == "")      // 화면에 보이는 영역이 비었을 때
            {
                isInput = true;
                chats[i].text = chat;
                break;      // 입력 마치면 반복문 탈출
            }
        }
        if (isInput == false)        // 더 이상 입력 값을 넣지 못할 경우 한칸 밀려 쓰기, 아래 공간에 입력
        {
            for (int i = 1; i < chats.Length; i++)
            {
                chats[i - 1].text = chats[i].text;
            }
            chats[chats.Length - 1].text = chat;
        }
    }
    #endregion

    public void JoinRandomRoom()        // 방이 있을 수도 없을 수 도
    {
        PhotonNetwork.JoinRandomRoom();
    }
    
    

    private void Update()
    {
        //print(PhotonNetwork.NetworkClientState);
    }
}
