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
    private bool result1 = false; //���� �������
    private bool result2 = false;   //�������� ��������
    private AudioSource bunnySound;
    private int level;

    private void Start()
    {
        level = PlayerPrefs.GetInt("Level", 1);
        this.gameObject.transform.position = new Vector3(Mathf.Pow(level, 2) * 300, 5, 0);
        bunny = GameObject.FindWithTag("Bunny");
        tm = turtle.GetComponent<TurtleMovement>();
        bm = bunny.GetComponent<BunnyMovement>();
        turtleAnim = turtle.GetComponent<Animator>();
        bunnyAnim = bunny.GetComponent<Animator>();
        bunnySound = bunny.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bunnySound.Stop();
        if (other.transform.CompareTag("Player"))                                                       // ���� ������������� � �������
        {
            turtleWin = true;
            bunnyWin = false;
        }
        else if (other.transform.CompareTag("Bunny"))                                                   // ���� ������������� � ������
        {
            turtleWin = false;
            bunnyWin = true;
        }

        if (turtleWin && !bunnyWin)
        {
            Debug.Log("bunny lost");
            result2 = true;
            turtleAnim.SetInteger("win", 1);
            bunnyAnim.SetInteger("win", 0);
            level++;
            PlayerPrefs.SetInt("Level", level);
            
        }
        else if (!turtleWin && bunnyWin)
        {
            Debug.Log("Turtle lost");
            result1 = true;
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
