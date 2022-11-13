using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class killFeed : MonoBehaviour
{

    public Transform killFeedContainer; 
    public GameObject killFeedTemplate;
    public Photon.Pun.PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void addToKillFeed( string shooter, string deadPerson)
    {
        GameObject newKillFeedEntry = Instantiate(killFeedTemplate, killFeedContainer);
        newKillFeedEntry.GetComponent<killFeedText>()._killFeedText.text = shooter + " killed " + deadPerson;
        Destroy(newKillFeedEntry, 5);
    }

}
