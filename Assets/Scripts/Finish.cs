using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject bunny;
    public GameObject turtle;
    private Animator turtleAnim;
    private Animator bunnyAnim;
    private BunnyMovement bm;
    private TurtleMovement tm;

    private void Start()
    {
        tm = turtle.GetComponent<TurtleMovement>();
        bm = bunny.GetComponent<BunnyMovement>();
        turtleAnim = turtle.GetComponent<Animator>();
        bunnyAnim = bunny.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))                                                       // Если заколайдилось с игроком
        {
           if (tm.win)                                                                                  // И он победил...
            {
                bm.win = false;
                PlayerPrefs.SetInt("Level",
                    PlayerPrefs.GetInt("Level", 1) + 1);                                                // При победе открыть следующий уровень
                //Отыграть анимацию победы черепахи
            }
           else
            {
                //Отыграть анимацию поражения черепахи
            }
        }
        else if (other.transform.CompareTag("Bunny"))                                                   // Если заколайдилось с зайцем
        {
            if (bm.win)                                                                                 // И он победил
            {
                tm.win = false;
                //Отыграть победную анимацию зайца
            }
            else
            {
                // Отыграть анимацию поражения зайца
            }
            
        }

    }
}
