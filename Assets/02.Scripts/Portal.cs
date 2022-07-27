using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Portal : MonoBehaviourPunCallbacks
{
    GameObject netMgr;
    private void Start()
    {
        netMgr = GameObject.Find("NetworkManager");
    }
    //플레이어가 각 문과 충돌하면 해당하는 UI 활성화 
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Door_Aespa"))
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel(2);
            netMgr.GetComponent<NetworkManager>().ChangePanel(NetworkManager.ActivePanel.AESPA);
        }
        if (other.gameObject.name.Contains("Door_BTS"))
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel(5);
            netMgr.GetComponent<NetworkManager>().ChangePanel(NetworkManager.ActivePanel.BTS);
        }
        if (other.gameObject.name.Contains("Door_Twice"))
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel(8);
            netMgr.GetComponent<NetworkManager>().ChangePanel(NetworkManager.ActivePanel.TWICE);
        }
    }
}