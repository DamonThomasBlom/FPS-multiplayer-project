using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject spectatePrefab;
    public GameObject spectateSpawnPos;
    public GameObject playerPrefab;
    public globalConfig config;

    // Spawn Position
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float Y;


    private void Start()
    {
        config = FindObjectOfType<globalConfig>();

        //global config, singleplayer = true/false
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Y, Random.Range(minZ, maxZ));

        // Multiplayer
        if (config != null && !config.singlePlayer && !config.spectateMode)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        }

        // Single player 
        else if (config != null && config.singlePlayer && !config.spectateMode)
        {
            Vector3 spawnPos = new Vector3(-20, 1, -17);
            Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            Debug.Log("Player is spawning");
        }

        // Spectate mode 
        else if (config != null && !config.singlePlayer && config.spectateMode)
        {
            Instantiate(spectatePrefab, spectateSpawnPos.transform.position, Quaternion.identity);
            Debug.Log("Spectate Camera is spawning");
        }
        
    }
}
