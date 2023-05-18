using UnityEngine;


[System.Serializable]
public class BoosterObject
{
    public string name;
    public GameObject prefab;
    public float weight = 1;
    public float minY = -3.5f;
    public float maxY = 30.0f;



    void Start()
    {
        Upgrade();
    }
    void Upgrade()
    {
        weight+=PlayerPrefs.GetInt(name, 0);                    // —читываем сколько веса было добавлено из улучшений магазина и улучшаем префаб
    }
}
