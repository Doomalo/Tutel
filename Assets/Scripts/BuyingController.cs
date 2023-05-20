using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BuyingController : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject[] menusToCheck;
    public GameObject[] platformsMenus;
    public GameObject[] bonusMenus;
    public GameObject[] boosterMenus;
    public GameObject moneyText;
    public GameObject usingObject;


    public void TryBuy()
    {
        int money = 99999;//PlayerPrefs.GetInt("Money", 0);                                                 // ������� ����� �����
        string menuName = MenuCheck().name;
        if (menuName == "Platforms")
            foreach (GameObject parent in platformsMenus)                                               // ��������� ��� �������
            {
                if (parent.activeSelf)                                                                   // ������� ������ ��������
                {
                    int cost = int.Parse(parent.GetComponentInChildren<UnityEngine.UI.Text>().text);    // ������� ��������� � ������� �� ������ � � �������
                    Debug.Log(cost);
                    if ((money >= cost) && (PlayerPrefs.GetInt(parent.name, 0) != 1))                         //���� ����� ������� � ������� �� ��� ������
                    {
                        Debug.Log("Successfully bought");
                        PlayerPrefs.SetInt(parent.name, 1);
                        money -= cost;
                        PlayerPrefs.SetInt("Money", money);                                                 // �������� �� �������� ������
                        moneyText.GetComponent<UnityEngine.UI.Text>().text = money.ToString();
                    }
                }
            }
        else if (menuName == "Bonus")
            foreach (GameObject parent in bonusMenus)                                               // ��������� ��� �������
            {
                if (parent.activeSelf)                                                                   // ������� ������ ��������
                {
                    int cost = int.Parse(parent.GetComponentInChildren<UnityEngine.UI.Text>().text);    // ������� ��������� � ������� �� ������ � � �������
                    if (money >= cost)                         //���� �����
                    {
                        Debug.Log("Successfully bought");
                        int amount = PlayerPrefs.GetInt(parent.name, 0);
                        amount++;
                        Debug.Log(amount +" of " + parent.name);
                        PlayerPrefs.SetInt(parent.name, amount);
                        money -= cost;
                        PlayerPrefs.SetInt("Money", money);                                                 // �������� �� �������� ������
                        moneyText.GetComponent<UnityEngine.UI.Text>().text = money.ToString();
                    }
                }
            }
        else if (menuName == "Booster")
            foreach (GameObject parent in boosterMenus)                                               // ��������� ��� �������
            {
                if (parent.activeSelf)                                                                   // ������� ������ ��������
                {
                    int cost = int.Parse(parent.GetComponentInChildren<UnityEngine.UI.Text>().text);    // ������� ��������� � ������� �� ������ � � �������
                    Debug.Log(cost);
                    if ((money >= cost) && (PlayerPrefs.GetInt(parent.name, 0) != 1))                         //���� ����� ������� � ������� �� ��� ������
                    {
                        Debug.Log("Successfully bought");
                        PlayerPrefs.SetInt(parent.name, 1);
                        money -= cost;
                        PlayerPrefs.SetInt("Money", money);                                                 // �������� �� �������� ������
                        moneyText.GetComponent<UnityEngine.UI.Text>().text = money.ToString();
                    }
                }
            }
    }

    public void TryUse()
    {
        foreach (Transform p in MenuCheck().GetComponentInChildren<Transform>())                                               // ��������� ��� �������
        {
            GameObject go = p.gameObject;
            if (go.activeSelf)                                                                   // ������� ������ ��������
            {
                Debug.Log(go.name);
                if (PlayerPrefs.GetInt(go.name, 0) >= 1)                         //���� ������ ��� ������, �������� ���
                {
                    switch(go.name)
                    {
                        case ("Sling"):DataController.platformNum = 0;break;
                        case ("Donkey"): DataController.platformNum = 1; break;
                        case ("Catapult"): DataController.platformNum = 2; break;
                        case ("Bear"): DataController.platformNum = 3; break;
                        case ("Geyser"): DataController.platformNum = 4; break;
                        case ("Cannon"): DataController.platformNum = 5; break;
                        case ("Firework"): PlayerPrefs.SetInt("Booster", 5);PlayerPrefs.SetInt("BoosterTime", 5); break;
                        case ("Rocket"): PlayerPrefs.SetInt("Booster", 8); PlayerPrefs.SetInt("BoosterTime", 8); DataController.platformNum = 5; break;
                        case ("Fan"): PlayerPrefs.SetInt("FanUsed", 1); break;
                        case ("Pan"): PlayerPrefs.SetInt("PanUsed", 1); break;
                        case ("Ballons"): PlayerPrefs.SetInt("BoosterUsed", 1); break;
                    }    
                    Debug.Log("Successfully chose" + go.name);
                }
            }
        }
    }

    public GameObject MenuCheck()
    {
        foreach (GameObject parent in menusToCheck)                                               // ��������� ��� �������
        {
            if (parent.activeSelf)
            {
                usingObject = parent;
                //Debug.Log(usingObject.name);
                break;
            }                                                                                   // ������� ������ ��������
            
        }
        return usingObject;
    }
    public void loadLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
