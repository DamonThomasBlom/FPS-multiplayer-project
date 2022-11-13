using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class leaderboardTable : MonoBehaviourPunCallbacks
{
    public GameObject leaderBoard;
    public Transform entryContainer;
    public GameObject _entryTemplate;

    public Photon.Pun.PhotonView myPV;

    private void Start()
    {
        myPV = GetComponent<Photon.Pun.PhotonView>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(leaderBoard.activeInHierarchy == false)
            {
                leaderBoard.SetActive(true);

                foreach (playerScoreTracker player in FindObjectsOfType<playerScoreTracker>())
                {
                    generateScoreBoard(player, _entryTemplate);
                }
            }
            else
            {
                // Close leaderBoard 
                leaderBoard.SetActive(false);
                foreach (Transform child in entryContainer)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    public void generateScoreBoard(playerScoreTracker playerScore, GameObject entryTemplate)    
    {
        GameObject newEntry = Instantiate(_entryTemplate, entryContainer.transform);
        newEntry.GetComponent<leaderBoardEntry>().playerName.text = playerScore.playerName;
        newEntry.GetComponent<leaderBoardEntry>().deaths.text = playerScore.deaths.ToString();
        newEntry.GetComponent<leaderBoardEntry>().kills.text = playerScore.kills.ToString();
    }
    
}


