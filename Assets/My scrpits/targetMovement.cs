using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetMovement : MonoBehaviour
{
    public float movementRange;
    private float counter = 0;
    public float moveSpeed;
    private bool movingLeft;
    private bool movingRight;

    // Start is called before the first frame update
    void Start()
    {
        movingLeft = true;
        movingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingLeft)
        {
            counter -= Time.deltaTime;
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
        if (movingRight)
        {
            counter += Time.deltaTime;
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
        }
        if (counter <= -movementRange)
        {
            movingLeft = false;
            movingRight = true;
        }
        if (counter >= movementRange)
        {
            movingLeft = true;
            movingRight = false;
        }
    }
}
