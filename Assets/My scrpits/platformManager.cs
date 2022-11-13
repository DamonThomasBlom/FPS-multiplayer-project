using UnityEngine;

public enum platform
{
    android,
    windows
}

public class platformManager : MonoBehaviour
{ 
    public platform curPlatform;
    public bool androidDebug;

}
