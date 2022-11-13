using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectMap : MonoBehaviour
{
    SaveMap map;
    public string mapChoice = "map1";

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void selectMap1()
    {
        mapChoice = "map1"; 
    }

    public void selectMap2()
    {
        mapChoice = "map2";
    }

    public void selectMap3()
    {
        mapChoice = "map3";
    }
    
}
