using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BuyingController : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject[] menusToCheck;
    public GameObject[] platformsMenus;
    public GameObject moneyText;
    public GameObject usingObject;


    public void TryBuy()
    {
        int money = /*99999;*/PlayerPrefs.GetInt("Money", 0);                                                 // Текущая сумма денег
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
        foreach (Transform p in MenuCheck().GetComponentInChildren<Transform>())                                               // Проверяем все объекты
        {
            GameObject go = p.gameObject;
            if (go.activeSelf)                                                                   // находим объект активный
            {
                Debug.Log(go.name);
                if (PlayerPrefs.GetInt(go.name, 0) == 1)                         //если объект был куплен, выбираем его
                {
                    switch(go.name)
                    {
                        case ("Sling"):DataController.platformNum = 0;break;
                        case ("Donkey"): DataController.platformNum = 1; break;
                        case ("Catapult"): DataController.platformNum = 2; break;
                        case ("Bear"): DataController.platformNum = 3; break;
                        case ("Geyser"): DataController.platformNum = 4; break;
                        case ("Cannon"): DataController.platformNum = 5; break;
                    }    
                    Debug.Log("Successfully chose" + go.name);
                }
            }
        }
    }

    public GameObject MenuCheck()
    {
        foreach (GameObject parent in menusToCheck)                                               // Проверяем все объекты
        {
            if (parent.activeSelf)
            {
                usingObject = parent;
                Debug.Log(usingObject.name);
                break;
            }                                                                                   // находим объект активный
            
        }
        return usingObject;
    }
    public void loadLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
