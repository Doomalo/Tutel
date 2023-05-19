using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingController : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject[] menusToCheck;
    public GameObject[] platformsMenus;
    public GameObject moneyText;


    public void TryBuy()
    {
        int money = 15;//PlayerPrefs.GetInt("Money", 0);                                                 // Текущая сумма денег
        foreach (GameObject parent in platformsMenus)                                               // Проверяем все объекты
        {
            if (parent.activeSelf)                                                                   // находим объект активный
            {
                int cost = int.Parse(parent.GetComponentInChildren<UnityEngine.UI.Text>().text);    // смотрим стоимость и выводим на всякий её в консоль
                Debug.Log(cost);
                if ((money >= cost) && (PlayerPrefs.GetInt(parent.name, 0) != 1))                         //если денег хватает и предмет не был куплен
                {
                    Debug.Log("Successfully bought");
                    PlayerPrefs.SetInt(parent.name, 1);
                    money -= cost;
                    PlayerPrefs.SetInt("Money", money);                                                 // покупаем ии отнимаем деньги
                    moneyText.GetComponent<UnityEngine.UI.Text>().text = money.ToString();
                }
            }
        }
    }

    public void TryUse()
    {

    }

    public int MenuCheck()
    {
        foreach (GameObject parent in menusToCheck)                                               // Проверяем все объекты
        {
            if (parent.activeSelf)                                                                   // находим объект активный
                break;
        }
        return 0;
    }
}
