using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dummieMovement : MonoBehaviour
{
    public float movementRange;
    private float counter = 0;
    public float moveSpeed;
    private bool movingLeft;
    private bool movingRight;

    public int dummyHealth = 100;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        movingLeft = true;
        movingRight = false;
        if (slider != null)
            slider.value = dummyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(movingLeft)
        {
            counter -= Time.deltaTime;
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }
        if (movingRight)
        {
            counter += Time.deltaTime;
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
        if(counter <= -movementRange)
        {
            movingLeft = false;
            movingRight = true;
        }
        if(counter >= movementRange)
        {
            movingLeft = true;
            movingRight = false;
        }
    }

    public void takeDamage(int damage)
    {
        dummyHealth -= damage;
        if (dummyHealth <= 0)
            dummyHealth = 100;

        // Update the slider whenever the dummy takes damage
        slider.value = dummyHealth;
    }
}
