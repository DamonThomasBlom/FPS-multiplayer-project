using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectData : MonoBehaviour
{

    public static Obstacle getObjectsData(GameObject inpObj)
    {
        string type;
        float posX;
        float posY;
        float posZ;

        type = inpObj.name;
        posX = inpObj.transform.position.x;
        posY = inpObj.transform.position.y;
        posZ = inpObj.transform.position.z;

        Obstacle obstacle = new Obstacle();
        obstacle.type = type;
        obstacle.posX = posX;
        obstacle.posY = posY;
        obstacle.posZ = posZ;

    return obstacle;
    }
   
}
