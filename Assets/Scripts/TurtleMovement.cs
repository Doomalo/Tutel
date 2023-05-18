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
    public float reloading = 0;// ������� ����� ����� �������
    private bool flying = false;
    private bool flyingNow = false;
    public bool slam = false;
    public bool isLaunched = false;
    public float flyingSpeed;
    public float skyHight = 30.0f;                      // ��� �������� � ����, �� �������� �� ���� ������

    private bool boosterIsReady;

    public bool win = true;

    public float timer = -1.0f;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetBool("launched", false);
        _anim.SetBool("high_enough", false);
        _anim.SetBool("is_falling", false);
    }

    void FixedUpdate()
    {
        if (timer == 0)
            speedX-= PlayerPrefs.GetInt("Booster");
        timer -= Time.deltaTime;

        if (isLaunched)
        {
            _anim.SetBool("launched", true);
            reloading++;                                                                     // ���� �����������
            transform.Translate(speedX / 100.0f, speedY / 100.0f, 0);                        // ���� �����������
            speedX *= 0.9995f;                                                                // ���������� � �����
            if (!flying)                                                                     // ���� �� �� ����� (�������� ������ 40)
            {
                _anim.SetBool("high_enough", false);
                _anim.SetBool("is_falling", true);
                if (Input.GetButton("Fire1"))                             // ���� � �����
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
                    _anim.SetBool("is_falling", false);
                    //  Debug.Log("false");
                    flyingNow = true;
                }
            }
            else if (flying)
            {
                _anim.SetBool("is_falling", false);
                speedY = 0;                                                                     //�� ����� ����� �� �� ������
                if (Input.GetKey(KeyCode.UpArrow))                                                   //���� ����� �� ���  �������� �� ������!!!!!!!!!
                {
                    transform.Translate(0, flyingSpeed / 100.0f, 0);
                }
                if (Input.GetKey(KeyCode.DownArrow))                                                  //���� ���� �� ���  �������� �� ������ !!!!!!!!!!!!!!
                {
                    transform.Translate(0, -flyingSpeed / 100.0f, 0);
                }
                if (Input.GetButton("Fire1") && boosterIsReady) 
                {
                    boosterIsReady = false;
                    speedX += PlayerPrefs.GetInt("Booster");
                    timer = PlayerPrefs.GetInt("BoosterTime");
                }
                if (speedX < 40)                                                                // ���� ���� � <40 ��������, �� ������
                    flying = false;
            }
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
    }

    public void Defeat()
    {
        // ��� ��������� ��������� ���� ������ + ����������� ��������
        Destroy(this);
    }
}