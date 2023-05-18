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
        if (other.transform.CompareTag("Player"))                                                       // ���� ������������� � �������
        {
           if (tm.win)                                                                                  // � �� �������...
            {
                bm.win = false;
                PlayerPrefs.SetInt("Level",
                    PlayerPrefs.GetInt("Level", 1) + 1);                                                // ��� ������ ������� ��������� �������
                //�������� �������� ������ ��������
            }
           else
            {
                //�������� �������� ��������� ��������
            }
        }
        else if (other.transform.CompareTag("Bunny"))                                                   // ���� ������������� � ������
        {
            if (bm.win)                                                                                 // � �� �������
            {
                tm.win = false;
                //�������� �������� �������� �����
            }
            else
            {
                // �������� �������� ��������� �����
            }
            
        }

    }
}
