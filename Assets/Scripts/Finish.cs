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
    private bool turtleWin = false;
    private bool bunnyWin = false;
    private bool result1 = false; //заяц выиграл
    private bool result2 = false;   //черепаха выиграла
    private AudioSource bunnySound;

    private void Start()
    {
        tm = turtle.GetComponent<TurtleMovement>();
        bm = bunny.GetComponent<BunnyMovement>();
        turtleAnim = turtle.GetComponent<Animator>();
        bunnyAnim = bunny.GetComponent<Animator>();
        bunnySound = bunny.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bunnySound.Stop();
        if (other.transform.CompareTag("Player"))                                                       // Если заколайдилось с игроком
        {
            turtleWin = true;
            bunnyWin = false;
        }
        else if (other.transform.CompareTag("Bunny"))                                                   // Если заколайдилось с зайцем
        {
            turtleWin = false;
            bunnyWin = true;
        }


        if (turtleWin && !bunnyWin)
        {
            Debug.Log("bunny lost");
            result2 = true;
            bm.SetSpeed(0.0f);
            turtleAnim.SetInteger("win", 1);
            bunnyAnim.SetInteger("win", 0);
        }
        else if (!(turtleWin && !bunnyWin))
        {
            Debug.Log("Turtle lost");
            result1 = true;
            bm.SetSpeed(0.0f);
            turtleAnim.SetInteger("win", 0);
            bunnyAnim.SetInteger("win", 1);
        }
    }

    public bool ReturnResult1()
    {
        return result1;
    }

    public bool ReturnResult2()
    {
        return result2;
    }
}
