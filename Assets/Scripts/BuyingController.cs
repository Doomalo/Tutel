using System.Collections;
using System.Collections.Generic;
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
        int money = 15;//PlayerPrefs.GetInt("Money", 0);                                                 // ������� ����� �����
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
    }

    public void TryUse()
    {
        foreach (Transform p in MenuCheck().GetComponentInChildren<Transform>())                                               // ��������� ��� �������
        {
            GameObject go = p.gameObject;
            if (go.activeSelf)                                                                   // ������� ������ ��������
            {
                Debug.Log(go.name);
                if (PlayerPrefs.GetInt(go.name, 0) == 1)                         //���� ������ ��� ������, �������� ���
                {
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
                break;
            }                                                                                   // ������� ������ ��������
               
        }
        return usingObject;
    }
}
