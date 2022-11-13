using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spectateNetworkManager : MonoBehaviour
{

    private globalConfig config;
    private networkManager network;

    // Start is called before the first frame update
    void Start()
    {
        config = FindObjectOfType<globalConfig>();
        network = FindObjectOfType<networkManager>();

        if (config.spectateMode)
        {
            network.connectToServer();
        }
    }
}
