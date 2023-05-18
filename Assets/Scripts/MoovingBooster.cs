using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoovingBooster : MonoBehaviour
{
    public float speed = 1.0f;
    //private Transform transform;
   
    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(speed, 0);
    }
}
