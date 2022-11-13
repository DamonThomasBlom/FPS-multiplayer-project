using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructions : MonoBehaviour
{
    private globalConfig config;

    // Start is called before the first frame update
    void Start()
    {
        config = FindObjectOfType<globalConfig>();

        if (config.spectateMode == false)
        {
            gameObject.SetActive(false);
        }
        else if (config.spectateMode == true)
        {
            Invoke("hideInstructions", 10);
        }
    }

    public void hideInstructions()
    {
        gameObject.SetActive(false);
    }

}
