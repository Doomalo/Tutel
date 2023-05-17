using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    AddForce, SetValue
}

public class Booster : MonoBehaviour
{
    public float XValue;// ���������� � -1 ��� ����, ����� ��� ���������� �� ����������
    public float YValue; // ���������� � -1 ��� ����, ����� ��� ���������� �� ����������
    public Type type;
    private TurtleMovement tm;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))                                                       // ���� ������������� � �������
        {
            tm = other.gameObject.GetComponent<TurtleMovement>();                                       // �������� ��������� ������� �� ��� ��������
            tm.slam = false;                                                                            // ��������� ������� ���� ��� ����
            if (type == Type.AddForce)                                                                  // ��������� ���������
            {
                if (XValue != -1)
                    tm.speedX += XValue;
                if (YValue != -1)
                    tm.speedY += YValue;
            }
            else
                if(type==Type.SetValue)                                                                 // ������������� ���������
            {
                if (XValue != -1)
                    tm.speedX = XValue;
                if (YValue != -1)
                    tm.speedY = YValue;
            }
            this.gameObject.transform.tag = "DestroyThis";                                                                     // ����� �������� ������� ������
        }

    }
}
