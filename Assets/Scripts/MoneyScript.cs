using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("Money", 
                PlayerPrefs.GetInt(("Money"), 0)                                    // ≈сли задели монетку, увеличить кол-во денег на 1
                );
        }
    }
}
