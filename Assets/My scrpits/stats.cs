using Newtonsoft.Json;

public class stats 
{
    [JsonProperty("name")]
    public string playerName;

    [JsonProperty("deaths")]
    public string deaths;

    [JsonProperty("kills")]
    public string kills;
}

public class statsList
{
    stats[] statisticsList;
}
