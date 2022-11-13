using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerHealth : MonoBehaviour
{ 
    public TMPro.TextMeshProUGUI playerHealthUI;

    public string playerName;
    public string inputName;
    public float playerHealth = 100f;
    public Transform player;
    private GameObject[] spawnPoints;
    public PhotonView view;
    public leaderboardTable _leaderBoardTable;

    // Health bar
    public Slider slider;


    private void Start()
    {
        _leaderBoardTable = FindObjectOfType<leaderboardTable>();

        view = GetComponent<PhotonView>();

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        slider.value = playerHealth;

        if (view.IsMine)
        {
            playerName = PlayerPrefs.GetString("playername");
        }
    }

    private void Update()
    {
        if (view.IsMine)
        {
            //Update player health
            playerHealthUI.text = "Health: " + playerHealth;

            

            // When player health reaches zero
            if (playerHealth <= 0)
            {
                view.RPC("incDeaths", RpcTarget.All);
                respawnPlayer();
                Debug.Log("You Died!");
            }
        }  
        else if (!view.IsMine)
        {
            slider.value = playerHealth;
        }
    }
        
    
    // When player gets hit by bullet
    [PunRPC]
    void takeDamage(int damage)
    {
        playerHealth -= damage;
    }

    [PunRPC]
    void changeHealth(int newHealth)
    {
        playerHealth = newHealth;
    }

    void respawnPlayer()
    {
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        // Respawn player in one of the spawn points
        player.transform.position = spawnPoints[randomSpawnPoint].transform.position;
        view.RPC("changeHealth", RpcTarget.All, 100);
        playerHealth = 100;
    }
}
