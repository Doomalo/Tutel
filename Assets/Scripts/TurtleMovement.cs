using UnityEngine;
using System.Collections;
public class TurtleMovement : MonoBehaviour
{
    public float speedX = 30.0f;//сменить на приват
    public float speedY = 0.0f; //сменить на приват
    public float maxSpeed = 200.0f;
    public float gravity = 9.8f;
    public float maxY = 20.0f; // Объект движется между этими точками.
    public float minY = -4.5f;
    public float reload = 100;// Пауза между ударами
    private float reloading = 0;// Текущее время между ударами
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
            reloading++;                                                                     // Тики перезарядки
            transform.Translate(speedX / 100.0f, speedY / 100.0f, 0);                        // Само перемещение
            speedX *= 0.999f;                                                                // Замедление в полёте
            if (!flying)                                                                     // Если мы не летим (скорость меньше 40)
            {
                if (Input.GetButtonDown("Fire1") && (slam == false))                             // Удар в пол
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
                    flyingNow = true;
                }
            }
            if (flyingNow)                                                                       //Перед стадией полёта, у нас стадия взлёта до момента неба
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
                speedY = 0;                                                                     //Во время полёта мы не падаем
                if (Input.GetButton("Fire1"))                                                   //Полёт вверх на ЛКМ  ЗАМЕНИТЬ НА КНОПКИ!!!!!!!!!
                {
                    transform.Translate(0, flyingSpeed / 100.0f, 0);
                }
                if (Input.GetButton("Fire2"))                                                   //Полёт вниз на ПКМ  ЗАМЕНИТЬ НА КНОПКИ !!!!!!!!!!!!!!
                {
                    transform.Translate(0, -flyingSpeed / 100.0f, 0);
                }
                if (speedX < 40)                                                                // Если ушли в <40 скорость, то падаем
                    flying = false;
            }
        }
    }

    public void Defeat()
    {

    }
}