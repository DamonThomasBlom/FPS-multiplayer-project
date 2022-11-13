using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class mainMenu : MonoBehaviourPunCallbacks
{
    platformManager platform_manager;

    public void Start()
    {
        platform_manager = FindObjectOfType<platformManager>();
    }

    public void PlayGame()
    {
        if(platform_manager.curPlatform == platform.android)
        {
            SceneManager.LoadScene("FPS_ANDROID");
        }

        if (platform_manager.curPlatform == platform.windows)
        {
            SceneManager.LoadScene("FPS_PC SINGLEPLAYER");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
