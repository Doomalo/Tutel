using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StartLaunch : MonoBehaviour // �������� ����� � �������, ����� ��������, ����� ����������� ���, �������� � ����������� ��� ���������.
{
    private TurtleMovement tm;
    private BunnyMovement bm;
    private static Image StrengthBarImage;
    public GameObject turtle;
    public GameObject bunny;
    public float launchSpeed;
    public float barSpeed;
    public float bar = 0.0f;
    private float strength = 0.0f;
    private float direction = 0.0f;
    void Start()
    {
        StrengthBarImage = GetComponent<Image>();
        tm = turtle.gameObject.GetComponent<TurtleMovement>();
        bm = bunny.gameObject.GetComponent<BunnyMovement>();
        bm.enabled = false;
    }

    void FixedUpdate()
    {
        bar += barSpeed;
        if (bar >= 100.0f||bar<=0.0f)
            barSpeed *= -1.0f;
        StrengthBarImage.fillAmount = bar / 100.0f ;/// 100.0f;
        if (Input.GetButton("Fire1"))
            if (!tm.isLaunched)
                Launch();
    }
    void Launch()
    {
        //100-2*������� ����� 0, 100-0 ����� 100|������ 50 � ������� ��������� � 0 � 50-�
        strength = (100.0f - 2.0f * Mathf.Abs(bar - 50.0f)) / 100.0f;
        strength = Mathf.Clamp01(strength);// �������� ���� ������� � ��������� �� 0 �� 1;
        direction = bar / 10 * 9 * Mathf.PI / 180.0f;//�������� ����� � �������, ����� � �������;
        bm.enabled = true;
        tm.isLaunched = true;
        tm.speedX = launchSpeed * strength*Mathf.Cos(direction);//� = �������� ����������*����*����������� !!!!!!!!!!!!!!!!!!!�������� ���� ����� ������, �.�. bar � ��� ������ �� �����������, ��� � ��� ������ �� ����
        tm.speedY = launchSpeed * strength * Mathf.Sin(direction);//Y = �������� ����������*����*����������� !!!!!!!!!!!!!!!!!!!�������� ���� ����� ������, �.�. bar � ��� ������ �� �����������, ��� � ��� ������ �� ����
        StrengthBarImage.enabled = false;

    }
}
