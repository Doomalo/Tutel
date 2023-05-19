using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{

    private void Start()
    {

        int money = PlayerPrefs.GetInt("Money", 0);                                         // ��������� ����� �������� �����
        this.gameObject.GetComponent<UnityEngine.UI.Text>().text = money.ToString();// ���������� ����� �������� � ���� ������
    }
    public void UpdateMoney()
    {
        
            int money = PlayerPrefs.GetInt("Money",0);
            money++;                                                                // ���� ������ �������, ��������� ���-�� ����� �� 1
        PlayerPrefs.SetInt("Money", money);                                         // ��������� ����� �������� �����
        this.gameObject.GetComponent<UnityEngine.UI.Text>().text = money.ToString();// ���������� ����� �������� � ���� ������

        
    }
}
