using UnityEngine;
using System.Collections;
public class TurtleMovement : MonoBehaviour
{
    public float speedX = 30.0f;//������� �� ������
    public float speedY = 0.0f; //������� �� ������
    public float maxSpeed = 200.0f;
    public float gravity = 9.8f;
    public float maxY = 20.0f; // ������ �������� ����� ����� �������.
    public float minY = -4.5f;
    public float reload = 100;// ����� ����� �������
    private float reloading = 0;// ������� ����� ����� �������
    private bool flying = false;
    private bool flyingNow = false;
    public bool slam = false;
    public bool isLaunched = false;
    public float flyingSpeed;
    public float skyHight = 10.0f;
    void FixedUpdate()
    {
        if (isLaunched)
        {
            reloading++;                                                                     // ���� �����������
            transform.Translate(speedX / 100.0f, speedY / 100.0f, 0);                        // ���� �����������
            speedX *= 0.999f;                                                                // ���������� � �����
            if (!flying)                                                                     // ���� �� �� ����� (�������� ������ 40)
            {
                if (Input.GetButtonDown("Fire1") && (slam == false))                             // ���� � ���
                {
                    slam = true;
                    speedY = -30;
                }
                speedY -= gravity;
                if (transform.position.y < minY && ((reloading > reload) || (slam == true))) //���� ������� = ����� � ���� �� ����: ����������� ����������� ��� �� ������ �����
                {
                    reloading = 0;
                    speedY *= -0.9f;
                    if (!slam)                                                                // ���� �� � ��������� �����, �� ����������� �� ���
                        speedX -= 10;
                    slam = false;
                    if (speedX < 0)                                                           // ���� ���� � ������������� ��������, ����� ���� !!!!!!!!!!!!!!!!!!!!!!!!!
                        Defeat();
                }
                if (speedX > 40)
                {// ���� ���� � +40 ��������, �� ��������
                    flyingNow = true;
                }
            }
            if (flyingNow)                                                                       //����� ������� �����, � ��� ������ ����� �� ������� ����
            {
                if (transform.position.y < skyHight)
                    transform.Translate(10.0f / 100.0f, 20.0f / 100.0f, 0);
                else
                {
                    flyingNow = false;
                    flying = true;
                }
            }
            if (flying)
            {
                speedY = 0;                                                                     //�� ����� ����� �� �� ������
                if (Input.GetButton("Fire1"))                                                   //���� ����� �� ���  �������� �� ������!!!!!!!!!
                {
                    transform.Translate(0, flyingSpeed / 100.0f, 0);
                }
                if (Input.GetButton("Fire2"))                                                   //���� ���� �� ���  �������� �� ������ !!!!!!!!!!!!!!
                {
                    transform.Translate(0, -flyingSpeed / 100.0f, 0);
                }
                if (speedX < 40)                                                                // ���� ���� � <40 ��������, �� ������
                    flying = false;
            }
        }
    }

    public void Defeat()
    {

    }
}