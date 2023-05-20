using UnityEngine;
using System.Collections;
public class TurtleMovement : MonoBehaviour
{
    private Animator _anim;
    public GameObject finish;
    public float speedX = 30.0f;//сменить на приват
    public float speedY = 0.0f; //сменить на приват
    public float maxSpeed = 200.0f;
    public float gravity = 9.8f;
    public float maxY = 50.0f; // Объект движется между этими точками.
    public float minY = -4.5f;
    public float reload = 1;// Пауза между ударами
    private float reloading = 0;// Текущее время между ударами
    private Finish fin;
    public bool flying = false;
    public bool flyingNow = false;
    public bool slam = false;
    public bool isLaunched = false;
    public float flyingSpeed;
    public float skyHight = 30.0f;                      // При переходе в полёт, мы взлетаем до этой высоты
    public float slamReloadTime= 50;
    private float slamReloading = 0;
    private bool result1 = false;
    private bool result2 = false;
    private bool noMovement = false;


    private bool flyUp;
    private bool flyDown;
    public bool timerExpired = false;
    public bool boosterIsReady = true;

    private AudioSource audioSource;
    public AudioClip fall;

    public float timer = -1.0f;

    void Start()
    {
        fin = finish.GetComponent<Finish>(); 
        _anim = GetComponent<Animator>();
        _anim.SetBool("launched", false);
        _anim.SetBool("high_enough", false);
        _anim.SetBool("is_falling", false);
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        result1 = fin.ReturnResult1();
        result2 = fin.ReturnResult2();

        if (result1 == true || result2 == true)
        {
            noMovement = true;
            speedX = 0;
            //minY = -4.5f;
            //speedY = 0;
        }
        else if (timer == 0 && !boosterIsReady && !timerExpired)         // Условие, что по истечению таймера, от нас отнимут скорость
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
            slamReloading++;// Тики перезарядки
            if(!flyingNow)
            transform.Translate(speedX / 100.0f, speedY / 100.0f, 0);                        // Само перемещение
            speedX *= 0.9995f;                                                                // Замедление в полёте
            if (speedX > 40)
            {// Если ушли в +40 скорость, то взлетаем
                _anim.SetBool("is_falling", false);
                flyingNow = true;
            }
            if (!flying)                                                                     // Если мы не летим (скорость меньше 40)
            {
                _anim.SetBool("high_enough", false);
                _anim.SetBool("is_falling", true);
                speedY -= gravity;
                if (transform.position.y < minY && ((reloading > reload) || (slam == true))) //Если позиция = земля и одно из двух: перезарядка закончилась или мы слэмим землю
                {
                    audioSource.PlayOneShot(fall);
                    reloading = 0;
                    speedY *= -0.9f;
                    if (!slam)                                                                // Если мы в состоянии удара, не замедляемся об пол
                        speedX -= 10;
                    slam = false;
                    if (speedX < 0 && (!result2))                                                           // Если ушли в отрицательную скорость, конец игры !!!!!!!!!!!!!!!!!!!!!!!!!
                    {
                        Defeat();
                        _anim.SetInteger("win", 0);
                    }
                    }
            }
            else if (flying)
            {
                _anim.SetBool("is_falling", false);
                speedY = 0;                                                                     //Во время полёта мы не падаем
                if (flyUp)                                                   //Полёт вверх на ЛКМ  ЗАМЕНИТЬ НА КНОПКИ!!!!!!!!!
                {
                    transform.Translate(0, flyingSpeed / 100.0f, 0);
                }
                if (flyDown)                                                  //Полёт вниз на ПКМ  ЗАМЕНИТЬ НА КНОПКИ !!!!!!!!!!!!!!
                {
                    transform.Translate(0, -flyingSpeed / 100.0f, 0);
                }
                if (speedX < 40)                                                                // Если ушли в <40 скорость, то падаем
                    flying = false;
            }
        }
    }

    public void FlyUpPress()
    {
        if (!noMovement)
        {
            flyUp = true;
            if (transform.position.y > maxY)
                flyUp = false;
        }
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
        if (!noMovement)
        {
            if (!flying)                                                                     // Если мы не летим (скорость меньше 40)
            {
                if ((slamReloading > slamReloadTime) && (!slam))                             // Удар в полёте
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
    }

    void FlyingNowCheck()
    {
        if (flyingNow)                                                                       //Перед стадией полёта, у нас стадия взлёта до момента неба
        {
            _anim.SetBool("is_falling", false);
            _anim.SetBool("high_enough", true);
            if (transform.position.y < skyHight)
                transform.Translate(speedX / 100.0f, 50.0f / 100.0f, 0);
            else
            {
                flyingNow = false;
                flying = true;
            }
        }
    }

    public void Defeat()
    {
        // При поражении открывать меню смерти + проигрывать анимацию
        Destroy(this);
    }
}