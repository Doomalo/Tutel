using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyCreation : MonoBehaviour
{
    public GameObject[] bunnyPrefabs;
    private GameObject bunny;
    public GameObject launcher;
    private void Awake()
    {
        int bunnyId;
        bunnyId = PlayerPrefs.GetInt("Level", 1) - 1;
        bunnyId = Mathf.Clamp(bunnyId, 0, 3);
        bunny = Instantiate(bunnyPrefabs[bunnyId]) as GameObject;
        launcher.GetComponent<StartLaunch>().bunny = bunny;
    }
    private void Start()
    {
        
        bunny.transform.position = gameObject.transform.position;
        bunny.gameObject.tag = "Bunny";
        Destroy(this.gameObject);
    }
}
