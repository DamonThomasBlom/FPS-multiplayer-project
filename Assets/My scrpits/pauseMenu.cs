using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{

    public GameObject pauseMenuPanel;
    public bool pauseMenuIsActive = false;
    //private PhotonView view;

    //private void Start()
    //{
    //    view = FindObjectOfType<PhotonView>();
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (pauseMenuIsActive == false)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pauseMenuPanel.SetActive(true);
                pauseMenuIsActive = true;
            }
        }
    }

    public void resumeButton()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuPanel.SetActive(false);
        pauseMenuIsActive = false;
    }

    public void exitButton()
    {

        SceneManager.LoadScene(0);

        // If Player is connected then disconnect them from photon network 
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
    }
}
