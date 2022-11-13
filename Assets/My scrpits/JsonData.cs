using Newtonsoft.Json;
using System.Collections.Generic;

public class Map_Data
{
    [JsonProperty("Map")]
    public string map_name;
    public List<Obstacle> map_obstacles = new List<Obstacle>();

}

public class Obstacle
{
    [JsonProperty("Type")]
    public string type;
    public float posX;
    public float posY;
    public float posZ;

}