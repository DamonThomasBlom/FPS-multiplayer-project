using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerScoreTracker : MonoBehaviour
{
    public apiManager _apiManager;
    public PhotonView view;
    public GameObject player;
    public globalConfig config;

    //STATS
    public string playerName;
    public int kills = 0;
    public int deaths = 0;

    public void Start()
    {
        config = FindObjectOfType<globalConfig>();
        view = GetComponent<PhotonView>();

        // If multiplayer
        if (config != null && !config.singlePlayer && !config.spectateMode)
        {
            playerName = view.Owner.NickName;
            _apiManager = FindObjectOfType<apiManager>();
        }    
    }

    [PunRPC]
    public void incDeaths()
    {
        deaths += 1;
        if (view.IsMine)
        {
            _apiManager.postStat(playerName, 1, 0); 
        }
    }

    [PunRPC]
    public void incKills()
    {
        kills += 1;
        if (view.IsMine)
        {
            _apiManager.postStat(playerName, 0, 1);    
        }       
    }
}
