using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using System;

public class playerNameTag : MonoBehaviourPun
{

    [SerializeField] private TextMeshProUGUI nameText;
    private PhotonView view;
    private string playerName;
    public globalConfig _globalConfig;


    void Start()
    {
        _globalConfig = FindObjectOfType<globalConfig>();

        view = GetComponent<PhotonView>();
        playerName = GetComponent<playerScoreTracker>().playerName;
        if (photonView.IsMine) { return;  }

        setName();
    }

    private void setName()
    {
        if(_globalConfig.singlePlayer == false)
        {
            nameText.text = view.Owner.NickName;
        }
    }
}
