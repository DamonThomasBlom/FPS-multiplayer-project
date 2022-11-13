using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public networkManager network_manager;

    public void Start()
    {
        network_manager = FindObjectOfType<networkManager>();
    }

    // Create or join an exsisting room
    public void joinOrCreateRoom()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
}
