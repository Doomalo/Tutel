using UnityEngine;
using System.Collections;
public class TurtleMovement : MonoBehaviour
{
    private Animator _anim;
    public float speedX = 30.0f;//сменить на приват
    public float speedY = 0.0f; //сменить на приват
    public float maxSpeed = 200.0f;
    public float gravity = 9.8f;
    public float maxY = 20.0f; // Объект движется между этими точками.
    public float minY = -4.5f;
    public float reload = 1;// Пауза между ударами
    public float reloading = 0;// Текущее время между ударами
    private bool flying = false;
    private bool flyingNow = false;
    public bool slam = false;
    public bool isLaunched = false;
    public float flyingSpeed;
    public float skyHight = 30.0f;                      // При переходе в полёт, мы взлетаем до этой высоты

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
            reloading++;                                                                     // Тики перезарядки
            transform.Translate(speedX / 100.0f, speedY / 100.0f, 0);                        // Само перемещение
            speedX *= 0.9995f;                                                                // Замедление в полёте
            if (!flying)                                                                     // Если мы не летим (скорость меньше 40)
            {
                _anim.SetBool("high_enough", false);
                _anim.SetBool("is_falling", true);
                if (Input.GetButton("Fire1"))                             // Удар в полёте
                {
                    slam = true;
                    speedY = -30;
                }
                speedY -= gravity;
                if (transform.position.y < minY && ((reloading > reload) || (slam == true))) //Если позиция = земля и одно из двух: перезарядка закончилась или мы слэмим землю
                {
                    reloading = 0;
                    speedY *= -0.9f;
                    if (!slam)                                                                // Если мы в состоянии удара, не замедляемся об пол
                        speedX -= 10;
                    slam = false;
                    if (speedX < 0)                                                           // Если ушли в отрицательную скорость, конец игры !!!!!!!!!!!!!!!!!!!!!!!!!
                        Defeat();
                }
                if (speedX > 40)
                {// Если ушли в +40 скорость, то взлетаем
                    _anim.SetBool("is_falling", false);
                    //  Debug.Log("false");
                    flyingNow = true;
                }
            }
            else if (flying)
            {
                _anim.SetBool("is_falling", false);
                speedY = 0;                                                                     //Во время полёта мы не падаем
                if (Input.GetKey(KeyCode.UpArrow))                                                   //Полёт вверх на ЛКМ  ЗАМЕНИТЬ НА КНОПКИ!!!!!!!!!
                {
                    transform.Translate(0, flyingSpeed / 100.0f, 0);
                }
                if (Input.GetKey(KeyCode.DownArrow))                                                  //Полёт вниз на ПКМ  ЗАМЕНИТЬ НА КНОПКИ !!!!!!!!!!!!!!
                {
                    transform.Translate(0, -flyingSpeed / 100.0f, 0);
                }
                if (Input.GetButton("Fire1") && boosterIsReady) 
                {
                    boosterIsReady = false;
                    speedX += PlayerPrefs.GetInt("Booster");
                    timer = PlayerPrefs.GetInt("BoosterTime");
                }
                if (speedX < 40)                                                                // Если ушли в <40 скорость, то падаем
                    flying = false;
            }
            if (flyingNow)                                                                       //Перед стадией полёта, у нас стадия взлёта до момента неба
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
        // При поражении открывать меню смерти + проигрывать анимацию
        Destroy(this);
    }
}