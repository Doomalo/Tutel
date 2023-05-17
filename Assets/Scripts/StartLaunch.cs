using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StartLaunch : MonoBehaviour // Добавить Нажал и держишь, когда отпустил, тогда считывается бар, стреляет в направлении где отпустили.
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
        //100-2*крайнее выдаёт 0, 100-0 выдаёт 100|Выдает 50 в крайних значениях и 0 в 50-и
        strength = (100.0f - 2.0f * Mathf.Abs(bar - 50.0f)) / 100.0f;
        strength = Mathf.Clamp01(strength);// Получили силу запуска в диапазоне от 0 до 1;
        direction = bar / 10 * 9 * Mathf.PI / 180.0f;//Перевели шкалу в градусы, потом в радианы;
        bm.enabled = true;
        tm.isLaunched = true;
        tm.speedX = launchSpeed * strength*Mathf.Cos(direction);//Х = скорость катапульты*силу*направление !!!!!!!!!!!!!!!!!!!Возможно силу стоит убрать, т.к. bar и так влияет на направление, что и так влияет на силу
        tm.speedY = launchSpeed * strength * Mathf.Sin(direction);//Y = скорость катапульты*силу*направление !!!!!!!!!!!!!!!!!!!Возможно силу стоит убрать, т.к. bar и так влияет на направление, что и так влияет на силу
        StrengthBarImage.enabled = false;

    }
}
