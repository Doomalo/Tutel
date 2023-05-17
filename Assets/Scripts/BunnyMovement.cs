using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMovement : MonoBehaviour
{
    public float speed;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(speed / 100.0f, 0, 0);                          //Заяц просто ходит с определённой скоростью, можно добавить проверку на финиш потом
    }
}
