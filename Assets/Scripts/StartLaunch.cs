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
    private Vector2 direction;

    Vector3 worldPosition;

    void Start()
    {
        StrengthBarImage = GetComponent<Image>();
        tm = turtle.gameObject.GetComponent<TurtleMovement>();
        bm = bunny.gameObject.GetComponent<BunnyMovement>();
        bm.enabled = false;
    }

    void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        bar += barSpeed;
        if (bar >= 100.0f||bar<=0.0f)
            barSpeed *= -1.0f;
        StrengthBarImage.fillAmount = bar / 100.0f ;/// 100.0f;
        if (Input.GetButtonUp("Fire1"))
            if (!tm.isLaunched)
                Launch();
    }
    void Launch()
    {
        //100-2*������� ����� 0, 100-0 ����� 100|������ 50 � ������� ��������� � 0 � 50-�
        strength = (100.0f - 2.0f * Mathf.Abs(bar - 50.0f)) / 100.0f;
        strength = Mathf.Clamp01(strength);// �������� ���� ������� � ��������� �� 0 �� 1;
        direction = worldPosition-turtle.transform.position;//�������� ����� � �������, ����� � �������;
        direction.Normalize();
        bm.enabled = true;
        tm.isLaunched = true;
        tm.speedX = launchSpeed * strength;//� = �������� ����������*����*����������� !!!!!!!!!!!!!!!!!!!�������� ���� ����� ������, �.�. bar � ��� ������ �� �����������, ��� � ��� ������ �� ����
        tm.speedY = launchSpeed * strength * direction.y ;//Y = �������� ����������*����*����������� !!!!!!!!!!!!!!!!!!!�������� ���� ����� ������, �.�. bar � ��� ������ �� �����������, ��� � ��� ������ �� ����
        StrengthBarImage.enabled = false;
    }

    public bool ReturnLaunchState()
    {
        return tm.isLaunched;
    }
}
