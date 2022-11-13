using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerNetworkLogic : MonoBehaviour
{
    public PhotonView view;

    //LOCAL OBJECTS
    public GameObject cameraGO;
    public PlayerMovementScript _playermovementscript;
    public MouseLookScript _mouselookscript;
    public GunInventory _guninventory;
    public globalConfig _config;

    // Start is called before the first frame update
    void Start()
    {
        _playermovementscript = GetComponent<PlayerMovementScript>();
        _mouselookscript = GetComponent<MouseLookScript>();
        _guninventory = GetComponent<GunInventory>();
        

        view = GetComponent<PhotonView>();

        _config = FindObjectOfType<globalConfig>();

        if (_config != null && _config.singlePlayer == false )//&& _config.spectateMode)
        {
            if (cameraGO != null)
            {
                if (!view.IsMine)
                {
                    //DISABLE SCRIPTS USED FOR LOCAL PLAYER if PLAYER IS NOT OURS
                    _playermovementscript.enabled = false;
                    _mouselookscript.enabled = false;
                    cameraGO.SetActive(false);
                    _guninventory.enabled = false;
                }
                else if (view.IsMine)
                {
                    // Do nothing
                }
            }
        }   
    }
}
