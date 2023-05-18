using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyCreation : MonoBehaviour
{
    public GameObject[] bunnyPrefabs;
    private GameObject bunny;

    private void Start()
    {
        bunny = Instantiate(bunnyPrefabs[PlayerPrefs.GetInt("Level", 1)-1]) as GameObject;
        bunny.transform.position = gameObject.transform.position;
        bunny.gameObject.tag = "Bunny";
        Destroy(this.gameObject);
    }
}
