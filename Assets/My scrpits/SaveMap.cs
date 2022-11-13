using Newtonsoft.Json;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class SaveMap : MonoBehaviour
{
    // Map choice
    selectMap map;

    // Obstacles prefabs 
    public GameObject Barrier;
    public GameObject Bean;
    public GameObject Wall_obstacle;

    // Maps
    public TextAsset Map_1;
    public TextAsset Map_2;
    public TextAsset Map_3;

    // Obstacle array
    public List<GameObject> currentObstacles = new List<GameObject>();

    private globalConfig _globalConfig;

    // Load map 1 if on android 
    platformManager platform_manager;

    private void Start()
    {
        _globalConfig = FindObjectOfType<globalConfig>();

        if (_globalConfig.singlePlayer == false)
        {
            platform_manager = FindObjectOfType<platformManager>();
            map = FindObjectOfType<selectMap>();

            if (platform_manager.curPlatform == platform.android || platform_manager.curPlatform == platform.windows)
                loadSelectedMap();
        }
    }

    private void Update()
    {
        // Save current map
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    saveMap();
        //    Debug.Log("Saved map");
        //}

        // Load current saved map
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    loadMap();
        //}
    }

    // Saves current map obstacles
    void saveMap()
    {
        
        Map_Data map_data = new Map_Data();

        map_data.map_name = "Map 1";

        //COLLECT OBJECTS TO SAVE AS OBSTICALS
        foreach( SaveObject obstacle in FindObjectsOfType<SaveObject>())
        {
            map_data.map_obstacles.Add(GetObjectData.getObjectsData(obstacle.gameObject));
        }

        var jsonString = JsonConvert.SerializeObject(map_data, Formatting.Indented);
        Debug.Log(jsonString);
        Debug.Log("map saved");

        System.IO.File.WriteAllText(@"C:\Users\SpeakGeek\Documents\Damon\Map1Json.json", jsonString);

    }

   
    // Loads prevoiusly saved map
    void loadMap()    
    {
        if (File.Exists(@"C:\Users\SpeakGeek\Documents\Damon\Map1Json.json"))
        {
            string json_string = File.ReadAllText(@"C:\Users\SpeakGeek\Documents\Damon\Map1Json.json");

            var loadMapData = JsonConvert.DeserializeObject<Map_Data>(json_string);

            foreach (Obstacle obstacle in loadMapData.map_obstacles)
            {
                Vector3 position = new Vector3(obstacle.posX, obstacle.posY, obstacle.posZ);

                // Barrier
                if (obstacle.type.Contains("Barrier"))
                {
                    Instantiate(Barrier, position, transform.rotation);
                }

                // Bean
                if (obstacle.type.Contains("Bean"))
                {
                    Instantiate(Bean, position, transform.rotation);
                }

                // Wall_obstacle
                if (obstacle.type.Contains("Wall_Obstacle"))
                {
                    Instantiate(Wall_obstacle, position, transform.rotation);
                }
            }
        }
    }


    // Load map selected in the menu
    void loadSelectedMap()
    {
        if (map == null)
        {
            return;
        }

        // Selected map 1
        if (map.mapChoice.Equals("map1"))
        {
            loadMapObstacles(Map_1);
        }

        // Selected map 2
        if (map.mapChoice.Equals("map2"))
        {
            loadMapObstacles(Map_2);
        }

        // Selected map 3
        if (map.mapChoice.Equals("map3"))
        {
            loadMapObstacles(Map_3);
        }
    }



    void loadMapObstacles(TextAsset map)
    {
        // Destroy all current obstacles
        destroyAllObstacles();

        // Convert text asset into string 
        string json_string = map.text;

        // Convert string in Map_Data and Obstacles
        var loadMapData = JsonConvert.DeserializeObject<Map_Data>(json_string);

        foreach (Obstacle obstacle in loadMapData.map_obstacles)
        {
            Vector3 position = new Vector3(obstacle.posX, obstacle.posY, obstacle.posZ);

            // Barrier
            if (obstacle.type.Contains("Barrier"))
            {
                GameObject newBarrier = (GameObject)Instantiate(Barrier, position, transform.rotation);
                currentObstacles.Add(newBarrier);
            }

            // Bean
            if (obstacle.type.Contains("Bean"))
            {
                GameObject newBean = (GameObject)Instantiate(Bean, position, transform.rotation);
                currentObstacles.Add(newBean);
            }

            // Wall_obstacle
            if (obstacle.type.Contains("Wall_Obstacle"))
            {
                GameObject newWall_Obstacle = (GameObject)Instantiate(Wall_obstacle, position, transform.rotation);
                currentObstacles.Add(newWall_Obstacle);
            }
        }
    }

    

    void destroyAllObstacles()
    {
        foreach (GameObject obstacle in currentObstacles)
        {
            Destroy(obstacle);
        }
        currentObstacles.Clear();
    }


    void readJSON(string inpJson)      
    {
        var newMapData = JsonConvert.DeserializeObject<Map_Data>(inpJson);

        foreach (Obstacle obstacle in newMapData.map_obstacles)
        {
            Debug.Log(obstacle);
        }
    }

    
}