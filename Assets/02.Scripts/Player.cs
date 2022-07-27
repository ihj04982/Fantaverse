using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
using System;

public class Player : MonoBehaviourPun
{
    #region PlayerMove
    //������Ʈ
    public CharacterController _charactercontroller;

    //������
    float inputX;
    float inputZ;
    Vector3 v_movement;
    public float moveSpeed;

    //�̸���
    public Image bubble, happy, angry, good, love, sad, wow; 
    //����
    public AudioSource happyAudio;
    //�ִϸ��̼�
    Animator anim;
    #endregion

    void Start()
    {
        //������Ʈ
        _charactercontroller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        //speed
        moveSpeed = 10f;

        bubble.enabled = false;
        happy.enabled = false;
        angry.enabled = false;
        good.enabled = false;
        love.enabled = false;
        sad.enabled = false;
        wow.enabled = false;

    }
    private void Update()
    {
        Move();

        if (photonView.IsMine == true)
        {
            if (Input.GetKeyDown("1"))      //����䰡 IsMine�̰�, Ű�� ���ȴٸ� �ڷ�ƾ�� ����
            {
                photonView.RPC("IEHappy", RpcTarget.AllBuffered);
            }
            if (Input.GetKeyDown("2"))      
            {
                photonView.RPC("IEAngry", RpcTarget.AllBuffered);
            }
            //Good
            if (Input.GetKeyDown("3"))
            {
                photonView.RPC("IEGood", RpcTarget.AllBuffered);
            }
            //Love
            if (Input.GetKeyDown("4"))
            {
                photonView.RPC("IELove", RpcTarget.AllBuffered);
            }
            //Sad
            if (Input.GetKeyDown("5"))
            {
                photonView.RPC("IESad", RpcTarget.AllBuffered);
            }
            //Wow
            if (Input.GetKeyDown("6"))
            {
                photonView.RPC("IEWow", RpcTarget.AllBuffered);
            }
        }
    }
    private void SendEmoji()
    {
     if(photonView.IsMine ==true)
        {
            photonView.RPC("Emoji", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    IEnumerator IEHappy()
    {
        anim.SetTrigger("Happy");
        happyAudio.Play();
        bubble.enabled = true;
        happy.enabled = true;
        yield return new WaitForSeconds(0.7f);
        happy.enabled = false;
        bubble.enabled = false;
    }
    [PunRPC]
    IEnumerator IEAngry()
    {
        anim.SetTrigger("Angry");
        bubble.enabled = true;
        angry.enabled = true;
        yield return new WaitForSeconds(0.7f);
        angry.enabled = false;
        bubble.enabled = false;
    }
    [PunRPC]
    IEnumerator IEGood()
    {
        anim.SetTrigger("Good");
        bubble.enabled = true;
        good.enabled = true;
        yield return new WaitForSeconds(0.7f);
        good.enabled = false;
        bubble.enabled = false;
    }
    [PunRPC]
    IEnumerator IELove()
    {
        anim.SetTrigger("Love");
        bubble.enabled = true;
        love.enabled = true;
        yield return new WaitForSeconds(0.7f);
        love.enabled = false;
        bubble.enabled = false;
    }
    [PunRPC]
    IEnumerator IESad()
    {
        anim.SetTrigger("Sad");
        bubble.enabled = true;
        sad.enabled = true;
        yield return new WaitForSeconds(0.7f);
        sad.enabled = false;
        bubble.enabled = false;
    }
    [PunRPC]
    IEnumerator IEWow()
    {
        anim.SetTrigger("Wow");
        bubble.enabled = true;
        wow.enabled = true;
        yield return new WaitForSeconds(0.7f);
        wow.enabled = false  ;
        bubble.enabled = false;
    }

    void Move()
    {
        if (photonView.IsMine == true)
        {
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");

            //input forward
            v_movement = _charactercontroller.transform.forward * inputZ;
            //char rotate
            _charactercontroller.transform.Rotate(Vector3.up * inputX * (100f * Time.deltaTime));
            //char move
            _charactercontroller.Move(v_movement * moveSpeed * Time.deltaTime);
        }
    }
}