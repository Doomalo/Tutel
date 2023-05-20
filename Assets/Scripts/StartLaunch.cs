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
    public GameObject startingPlatform;
    public float launchSpeed;
    public float barSpeed;
    private float bar = 0.0f;
    private float strength = 0.0f;
    private float directionAng;

    public List<AudioClip> platformSounds;

    Vector3 worldPosition;

    void Start()
    {
        int id = DataController.platformNum;
        SetLaunchSpeed(id);
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
       //bunny.transform.position = worldPosition;
        bar += barSpeed;
        if (bar >= 100.0f||bar<=0.0f)
            barSpeed *= -1.0f;
        StrengthBarImage.fillAmount = bar / 100.0f ;/// 100.0f;
        if (Input.GetButton("Fire1")||(Input.touchCount>=1))
            if (!tm.isLaunched)
                Launch();
    }
    void Launch()
    {
        int num = DataController.platformNum;
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(platformSounds[num]);
        //100-2*������� ����� 0, 100-0 ����� 100|������ 50 � ������� ��������� � 0 � 50-�
        Vector2 dirVector;
        strength = bar / 100.0f;
        strength = Mathf.Clamp01(strength);// �������� ���� ������� � ��������� �� 0 �� 1;
        dirVector = -turtle.transform.position + worldPosition;
         directionAng = Vector2.Angle(Vector2.right, dirVector);//�������� ����� � �������, ����� � �������;
        bm.enabled = true;
        tm.isLaunched = true;
        tm.speedX = launchSpeed * strength;
        tm.speedY = launchSpeed * strength * Mathf.Clamp(
            Mathf.Tan(Mathf.Deg2Rad * directionAng),
            - 1, 1);                                                    //Y = �������� ����������*����*����������� 
        StrengthBarImage.enabled = false;
    }

    public bool ReturnLaunchState()
    {
        return tm.isLaunched;
    }
    void SetLaunchSpeed(int id)
    {
        switch (id)
        {
            case 0:launchSpeed = 80; break;
            case 1: launchSpeed = 90; break;
            case 2: launchSpeed = 100; break;
            case 3: launchSpeed = 110; break;
            case 4: launchSpeed = 130; break;
            case 5: launchSpeed = 155; break;
        }
    }
}
