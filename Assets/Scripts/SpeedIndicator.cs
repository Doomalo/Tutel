using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeedIndicator : MonoBehaviour
{
    public TurtleMovement turtle;
    private double speed;

    private void FixedUpdate()
    {
        if (turtle)
            this.gameObject.GetComponent<UnityEngine.UI.Text>().text = ((int)(turtle.speedX)).ToString();
        else
            this.gameObject.GetComponent<UnityEngine.UI.Text>().text = "0";
    }
}
