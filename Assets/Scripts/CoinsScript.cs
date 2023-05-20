using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsScript : MonoBehaviour
{
    // Start is called before the first frame update
    public MoneyScript moneyController;
    private AudioSource audioSource;

    private void Start()
    {
        moneyController = GameObject.Find("количество монет").GetComponent<MoneyScript>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            {
            audioSource.Play();
            moneyController.UpdateMoney();
            //this.gameObject.transform.tag = "DestroyThis";
            Destroy(gameObject, 0.15f);
        }
    }
}
