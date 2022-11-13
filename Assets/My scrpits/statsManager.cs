using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statsManager : MonoBehaviour
{
    public Transform entryContainer;
    public GameObject entryTemplate;
    public List<stats> playerStatsList;
    public GameObject apiManager;

    private void Start()
    {
        playerStatsList = apiManager.GetComponent<apiManager>().allPlayerStats;
    }

    public void populateStats()
    {
        // First clear the entry container
        foreach (GameObject entry in entryContainer)
        {
            Destroy(entry);
        }

        // Populate the table 
        foreach (stats myStats in playerStatsList)
        {
            generateScoreBoard(myStats, entryTemplate);
        }
    }

    public void generateScoreBoard(stats playerScore, GameObject entryTemplate)
    {
        GameObject newEntry = Instantiate(entryTemplate, entryContainer.transform);
        newEntry.GetComponent<leaderBoardEntry>().playerName.text = playerScore.playerName;
        newEntry.GetComponent<leaderBoardEntry>().deaths.text = playerScore.deaths.ToString();
        newEntry.GetComponent<leaderBoardEntry>().kills.text = playerScore.kills.ToString();
    }
}
