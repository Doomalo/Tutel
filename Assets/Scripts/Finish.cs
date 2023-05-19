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
    private bool turtleWin;
    private bool bunnyWin;
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
            turtleAnim.SetInteger("win", 1);
            bunnyAnim.SetInteger("win", 0);
        }
        else
        {
            turtleAnim.SetInteger("win", 0);
            bunnyAnim.SetInteger("win", 1);
        }
    }
}
