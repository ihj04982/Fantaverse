using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video; // 비디오 플레이어 사용

public class VideoManager : MonoBehaviour
{
    public VideoPlayer myVideo;
    public int maxPlayer = 2;
    private void Start()
    {
        myVideo.Pause();
    }

    //방에 들어오면
    private void OnTriggerEnter(Collider other)
    {
        //Player가 maxPlayer 만큼 접속할 시
        if (GameObject.FindGameObjectsWithTag("Player").Length >= maxPlayer)
        {
            myVideo.Play(); 
        }
    }
}