using UnityEngine;
using System.Collections;
public class TurtleMovement : MonoBehaviour
{
    private Animator _anim;
    public float speedX = 30.0f;//������� �� ������
    public float speedY = 0.0f; //������� �� ������
    public float maxSpeed = 200.0f;
    public float gravity = 9.8f;
    public float maxY = 20.0f; // ������ �������� ����� ����� �������.
    public float minY = -4.5f;
    public float reload = 1;// ����� ����� �������
    private float reloading = 0;// ������� ����� ����� �������
    public bool flying = false;
    public bool flyingNow = false;
    public bool slam = false;
    public bool isLaunched = false;
    public float flyingSpeed;
    public float skyHight = 30.0f;                      // ��� �������� � ����, �� �������� �� ���� ������
    public float slamReloadTime= 50;
    private float slamReloading = 0;


    private bool flyUp;
    private bool flyDown;
    public bool timerExpired = false;
    public bool boosterIsReady = true;

    private AudioSource audioSource;
    public AudioClip fall;
    //public bool win = true;

    public float timer = -1.0f;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetBool("launched", false);
        _anim.SetBool("high_enough", false);
        _anim.SetBool("is_falling", false);
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (timer == 0 && !boosterIsReady && !timerExpired)         // �������, ��� �� ��������� �������, �� ��� ������� ��������
        {
            speedX -= PlayerPrefs.GetInt("Booster");
            timerExpired = true;
        }
        timer -= Time.deltaTime;
        FlyingNowCheck();
        if (isLaunched)
        {
            _anim.SetBool("launched", true);
            reloading++;
            slamReloading++;// ���� �����������
            transform.Translate(speedX / 100.0f, speedY / 100.0f, 0);                        // ���� �����������
            speedX *= 0.9995f;                                                                // ���������� � �����
            if (speedX > 40)
            {// ���� ���� � +40 ��������, �� ��������
                _anim.SetBool("is_falling", false);
                //  Debug.Log("false");
                flyingNow = true;
            }
            if (!flying)                                                                     // ���� �� �� ����� (�������� ������ 40)
            {
                _anim.SetBool("high_enough", false);
                _anim.SetBool("is_falling", true);
                speedY -= gravity;
                if (transform.position.y < minY && ((reloading > reload) || (slam == true))) //���� ������� = ����� � ���� �� ����: ����������� ����������� ��� �� ������ �����
                {
                    audioSource.PlayOneShot(fall);
                    reloading = 0;
                    speedY *= -0.9f;
                    if (!slam)                                                                // ���� �� � ��������� �����, �� ����������� �� ���
                        speedX -= 10;
                    slam = false;
                    if (speedX < 0)                                                           // ���� ���� � ������������� ��������, ����� ���� !!!!!!!!!!!!!!!!!!!!!!!!!
                        Defeat();
                }
            }
            else if (flying)
            {
                _anim.SetBool("is_falling", false);
                speedY = 0;                                                                     //�� ����� ����� �� �� ������
                if (flyUp)                                                   //���� ����� �� ���  �������� �� ������!!!!!!!!!
                {
                    transform.Translate(0, flyingSpeed / 100.0f, 0);
                }
                if (flyDown)                                                  //���� ���� �� ���  �������� �� ������ !!!!!!!!!!!!!!
                {
                    transform.Translate(0, -flyingSpeed / 100.0f, 0);
                }
                if (speedX < 40)                                                                // ���� ���� � <40 ��������, �� ������
                    flying = false;
            }
        }
    }

    public void FlyUpPress()
    {
         flyUp = true;
    }
    public void FlyUpRelease()
    {
        flyUp = false;
    }
    public void FlyDown()
    {
        flyDown = true;
    }
    public void FlyDownRelease()
    {
        flyDown = false;
    }

    public void SlamButton()
    {
        if (!flying)                                                                     // ���� �� �� ����� (�������� ������ 40)
        {
            if ((slamReloading > slamReloadTime) && (!slam))                             // ���� � �����
            {
                slamReloading = 0;
                slam = true;
                speedY = -30;
            }
        }
        else
        {
            if (boosterIsReady)
            {
                boosterIsReady = false;
                speedX += PlayerPrefs.GetInt("Booster");
                timer = PlayerPrefs.GetInt("BoosterTime");
                Debug.Log("Booster Activated");
            }
        }
    }

    void FlyingNowCheck()
    {
        if (flyingNow)                                                                       //����� ������� �����, � ��� ������ ����� �� ������� ����
        {
            _anim.SetBool("is_falling", false);
            _anim.SetBool("high_enough", true);
            if (transform.position.y < skyHight)
                transform.Translate(10.0f / 100.0f, 20.0f / 100.0f, 0);
            else
            {
                flyingNow = false;
                flying = true;
            }
        }
    }

    public void Defeat()
    {
        // ��� ��������� ��������� ���� ������ + ����������� ��������
        Destroy(this);
    }
}