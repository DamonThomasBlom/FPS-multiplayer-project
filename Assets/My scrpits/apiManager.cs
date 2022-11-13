using BestHTTP;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class apiManager : MonoBehaviour
{
    public List<stats> allPlayerStats = new List<stats>();
    //public UnityEvent statsReceived;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        getStats();
    }

    [Button]
    public void testGetStats()
    {
        getStats();
    }

    public void postStat(string name, int deaths, int kills)
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://188.166.154.232:3000/stats"), HTTPMethods.Post, OnRequestFinishedPost);

        //name
        request.AddField("name", name);

        //deaths
        request.AddField("deaths", deaths.ToString());

        //kills
        request.AddField("kills", kills.ToString());

        request.Send();
    }


    public void getStats()
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://188.166.154.232:3000/stats"), HTTPMethods.Get, OnRequestFinishedGet);

        request.Send();
    }


    private void OnRequestFinishedPost(HTTPRequest req, HTTPResponse resp)
    {

        switch (req.State)
        {
            // The request finished without any problem.
            case HTTPRequestStates.Finished:
                if (resp.IsSuccess)
                {
                    // Everything went as expected!
                    Debug.Log("StatusCode: " + resp.StatusCode);
                    Debug.Log("Message: " + resp.Message);
                    Debug.Log("DataAsText: " + resp.DataAsText);
                }
                else
                {
                    Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                    resp.StatusCode,
                                                    resp.Message,
                                                    resp.DataAsText));
                }
                break;

            // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
            case HTTPRequestStates.Error:
                Debug.LogError("Request Finished with Error! " + (req.Exception != null ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception"));
                break;

            // The request aborted, initiated by the user.
            case HTTPRequestStates.Aborted:
                Debug.LogWarning("Request Aborted!");
                break;

            // Connecting to the server is timed out.
            case HTTPRequestStates.ConnectionTimedOut:
                Debug.LogError("Connection Timed Out!");
                break;

            // The request didn't finished in the given time.
            case HTTPRequestStates.TimedOut:
                Debug.LogError("Processing the request Timed Out!");
                break;
        }

    }

    private void OnRequestFinishedGet(HTTPRequest req, HTTPResponse resp)
    {

        switch (req.State)
        {
            // The request finished without any problem.
            case HTTPRequestStates.Finished:
                if (resp.IsSuccess)
                {
                    //allPlayerStats.Clear();

                    // Everything went as expected!
                    Debug.Log("StatusCode: " + resp.StatusCode);
                    Debug.Log("Message: " + resp.Message);
                    Debug.Log("DataAsText: " + resp.DataAsText);

                    Debug.Log("REQUEST COMPLETED");

                    string json_string = resp.DataAsText;

                    stats[] inputStats = JsonConvert.DeserializeObject<stats[]>(json_string);

                    allPlayerStats.AddRange(inputStats);

                    //statsReceived.Invoke();
                }
                else
                {
                    Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                    resp.StatusCode,
                                                    resp.Message,
                                                    resp.DataAsText));
                }
                break;

            // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
            case HTTPRequestStates.Error:
                Debug.LogError("Request Finished with Error! " + (req.Exception != null ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception"));
                break;

            // The request aborted, initiated by the user.
            case HTTPRequestStates.Aborted:
                Debug.LogWarning("Request Aborted!");
                break;

            // Connecting to the server is timed out.
            case HTTPRequestStates.ConnectionTimedOut:
                Debug.LogError("Connection Timed Out!");
                break;

            // The request didn't finished in the given time.
            case HTTPRequestStates.TimedOut:
                Debug.LogError("Processing the request Timed Out!");
                break;
        }

    }
}