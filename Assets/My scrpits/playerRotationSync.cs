using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRotationSync : MonoBehaviour
{
    public Vector3 curRotation;
    public PhotonView myPV;

    private void Start()
    {
        myPV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (myPV.IsMine)
        {
            curRotation = transform.eulerAngles;
            myPV.RPC("syncRot", RpcTarget.Others, curRotation);
        }
    }

    [PunRPC]
    public void syncRot(Vector3 newRot)
    {
        curRotation = newRot;
        transform.eulerAngles = curRotation;
    }

}
