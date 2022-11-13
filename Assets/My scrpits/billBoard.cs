using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billBoard : MonoBehaviour
{
    private Transform mainCameraTransform;
    private globalConfig config;

    // Start is called before the first frame update
    void Start()
    {

        config = FindObjectOfType<globalConfig>();
        if (config.spectateMode)
        {
            mainCameraTransform = GameObject.FindGameObjectWithTag("spectateCamera").transform;
            return;
        }

        mainCameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward,
        mainCameraTransform.rotation * Vector3.up);
    }
}
