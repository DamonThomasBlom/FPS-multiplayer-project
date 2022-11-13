using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class globalConfig : MonoBehaviour
{
    public bool singlePlayer = true;
    public bool spectateMode = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void updateSinglePlayer(bool newVal)
    {
        singlePlayer = newVal;
    }

    public void setSpectateMode()
    {
        singlePlayer = false;
        spectateMode = true;
    }

    public void setPlayerName(InputField playerInputName)
    {
        PlayerPrefs.SetString("playername", playerInputName.text);
        PhotonNetwork.NickName = playerInputName.text;
    }
}
