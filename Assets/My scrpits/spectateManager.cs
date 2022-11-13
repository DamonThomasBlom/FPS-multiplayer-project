using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spectateManager : MonoBehaviour
{
    public globalConfig config;
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        config = FindObjectOfType<globalConfig>();
        if (config.spectateMode)
        {
            cam.SetActive(true);
            //PhotonNetwork.JoinRandomOrCreateRoom();
        }
    }

}
