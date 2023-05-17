using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    AddForce, SetValue
}

public class Booster : MonoBehaviour
{
    public float XValue;// Установить в -1 для того, чтобы эта переменная не изменялась
    public float YValue; // Установить в -1 для того, чтобы эта переменная не изменялась
    public Type type;
    private TurtleMovement tm;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))                                                       // Если заколайдилось с игроком
        {
            tm = other.gameObject.GetComponent<TurtleMovement>();                                       // Получаем компонент скрипта на бег черепахи
            tm.slam = false;                                                                            // Отключаем падение если оно было
            if (type == Type.AddForce)                                                                  // Добавляем параметры
            {
                if (XValue != -1)
                    tm.speedX += XValue;
                if (YValue != -1)
                    tm.speedY += YValue;
            }
            else
                if(type==Type.SetValue)                                                                 // Устанавливаем параметры
            {
                if (XValue != -1)
                    tm.speedX = XValue;
                if (YValue != -1)
                    tm.speedY = YValue;
            }
            this.gameObject.transform.tag = "DestroyThis";                                                                     // После коллизии удаляем бустер
        }

    }
}
