using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCam : MonoBehaviour
{
    /*
    FEATURES

        WASD/Arrows:    Movement
                  Q:    Climb
                  E:    Drop
              Shift:    Move faster
            Control:    Move slower
                End:    Toggle cursor locking to screen(you can also press Ctrl+P to toggle play mode on and off).
	*/
 
	public float cameraSensitivity = 90;
    public float climbSpeed = 4;
    public float normalMoveSpeed = 10;
    public float slowMoveFactor = 0.25f;
    public float fastMoveFactor = 3;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    // Boundaries
    public float maxX;
    public float minX;
    public float maxZ;
    public float minZ;

    private PhotonView view;
    private globalConfig config;

    private bool mouseLookEnabled;

    void Start()
    {
        config = FindObjectOfType<globalConfig>();
        if (!config.spectateMode) { return; }
        view = GetComponent<PhotonView>();
        Cursor.lockState = CursorLockMode.Locked;
        mouseLookEnabled = true;
    }

    void Update()
    {
        if (!view.IsMine && config.spectateMode == true && mouseLookEnabled)
        {

            #region Movement logic

            rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, -90, 90);

            transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
                transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
                transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
            }
            else
            {
                transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
                transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
            }


            if (Input.GetKey(KeyCode.Q)) { transform.position += transform.up * climbSpeed * Time.deltaTime; }
            if (Input.GetKey(KeyCode.E)) { transform.position -= transform.up * climbSpeed * Time.deltaTime; }

            if (Input.GetKeyDown(KeyCode.End))
            {
                Cursor.lockState = CursorLockMode.None;
            }

            #endregion

            #region Boundaries

            if (transform.position.x > maxX)
            {
                transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            }

            if (transform.position.x < minX)
            {
                transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            }

            if (transform.position.z > maxZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
            }

            if (transform.position.z < minZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
            }

            if (transform.position.y < 0.5)
            {
                transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            }

            #endregion

        }
    }

    private GameObject[] players;
    private int spectateNumber = -1;
    public Vector3 offset = new Vector3(-4, -1, 0);

    private void LateUpdate()
    {
        if (!view.IsMine && config.spectateMode == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (mouseLookEnabled)
                {
                    players = GameObject.FindGameObjectsWithTag("Player");
                    spectateNumber += 1;
                    if (spectateNumber == players.Length || spectateNumber > players.Length)
                    {
                        spectateNumber = -1;
                    }

                    if (spectateNumber == -1) { return; }

                    // Follow player 
                    setParent(players[spectateNumber]);
                }
                else
                {
                    setNoParent(players[spectateNumber]);
                }    
            }  
        }     
    }

    private void setParent(GameObject parent)
    {

        Transform cameraHolder = parent.GetComponent<spectateHolder>().spectateCameraHolder.transform;
            
        transform.SetParent(cameraHolder);
        transform.position = cameraHolder.transform.position;
        transform.rotation = cameraHolder.transform.rotation;
        mouseLookEnabled = false;
    }

    private void setNoParent(GameObject parent)
    {
        transform.SetParent(null);
        mouseLookEnabled = true;
    }
}
