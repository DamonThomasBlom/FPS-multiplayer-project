using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

public class networkManager : MonoBehaviourPunCallbacks
{
    public platformManager platform_manager;
    public UnityEvent connectedToPUN;

    #region Network

    private void Start()
    {
        platform_manager = FindObjectOfType<platformManager>();
    }

    public void connectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        //CONNECTED TO PUN
        connectedToPUN.Invoke();

    }

    #endregion

    public void CreateRoom(string roomToCreate)
    {
        PhotonNetwork.CreateRoom(roomToCreate);
    }

    public void JoinRoom(string roomToJoin)
    {
        PhotonNetwork.JoinRoom(roomToJoin);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("JOINING ROOM");

        if (platform_manager.curPlatform == platform.android)
        {
            PhotonNetwork.LoadLevel("FPS_ANDROID");
        }

        if (platform_manager.curPlatform == platform.windows)
        {
            PhotonNetwork.LoadLevel("FPS_PC");
        }
    }

    public override void OnErrorInfo(ErrorInfo errorInfo)
    {
        Debug.Log(errorInfo.Info);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }
}
