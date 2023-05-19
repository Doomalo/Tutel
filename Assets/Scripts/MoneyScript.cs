using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{

    private void Start()
    {

        int money = PlayerPrefs.GetInt("Money", 0);                                         // —охранить новое значение монет
        this.gameObject.GetComponent<UnityEngine.UI.Text>().text = money.ToString();// ”становить новое значение в поле текста
    }
    public void UpdateMoney()
    {
        
            int money = PlayerPrefs.GetInt("Money",0);
            money++;                                                                // ≈сли задели монетку, увеличить кол-во денег на 1
        PlayerPrefs.SetInt("Money", money);                                         // —охранить новое значение монет
        this.gameObject.GetComponent<UnityEngine.UI.Text>().text = money.ToString();// ”становить новое значение в поле текста

        
    }
}
