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
        //100-2*крайнее выдаёт 0, 100-0 выдаёт 100|Выдает 50 в крайних значениях и 0 в 50-и
        strength = (100.0f - 2.0f * Mathf.Abs(bar - 50.0f)) / 100.0f;
        strength = Mathf.Clamp01(strength);// Получили силу запуска в диапазоне от 0 до 1;
        direction = worldPosition-turtle.transform.position;//Перевели шкалу в градусы, потом в радианы;
        direction.Normalize();
        bm.enabled = true;
        tm.isLaunched = true;
        tm.speedX = launchSpeed * strength;//Х = скорость катапульты*силу*направление !!!!!!!!!!!!!!!!!!!Возможно силу стоит убрать, т.к. bar и так влияет на направление, что и так влияет на силу
        tm.speedY = launchSpeed * strength * direction.y ;//Y = скорость катапульты*силу*направление !!!!!!!!!!!!!!!!!!!Возможно силу стоит убрать, т.к. bar и так влияет на направление, что и так влияет на силу
        StrengthBarImage.enabled = false;
    }

    public bool ReturnLaunchState()
    {
        return tm.isLaunched;
    }
}
