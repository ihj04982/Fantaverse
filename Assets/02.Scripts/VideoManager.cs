using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video; // ���� �÷��̾� ���

public class VideoManager : MonoBehaviour
{
    public VideoPlayer myVideo;
    public int maxPlayer = 2;
    private void Start()
    {
        myVideo.Pause();
    }

    //�濡 ������
    private void OnTriggerEnter(Collider other)
    {
        //Player�� maxPlayer ��ŭ ������ ��
        if (GameObject.FindGameObjectsWithTag("Player").Length >= maxPlayer)
        {
            myVideo.Play(); 
        }
    }
}