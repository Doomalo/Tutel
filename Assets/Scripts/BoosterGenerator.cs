using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterGenerator : MonoBehaviour
{
    public List<BoosterObject> availableObjects;       // Создаваемые префабы

    public BoosterObject[] level2;                      // распихать объекты по уровням!!!
    public BoosterObject[] level3;
    public BoosterObject[] level4;

    private float screenWidthInPoints;

    public List<GameObject> objects;            // Созданные на данный момент префабы (нужно для дальнейшего удаления)
    public Transform turtle;
    public float objectsMinDistance = 10.0f;
    public float objectsMaxDistance = 20.0f;

    private float combinedWeight;

    private int level;

    private void Start()
    {
        level = PlayerPrefs.GetInt("Level", 1);                                                                 // Получаем уровень, на котором в данный момент находится человек
        switch(level)
        {
            case 4: foreach (BoosterObject bo in level4) availableObjects.Add(bo);goto case 3;                  // Если 4-ый уровень, в список доступных объектов добавляем объекты 4-ого уровня
            case 3: foreach (BoosterObject bo in level3) availableObjects.Add(bo);goto case 2;                  // С 2-ым и 3-им то же самое
            case 2: foreach (BoosterObject bo in level2) availableObjects.Add(bo); break;                       // Также добавляются все объекты нижних уровней (блоагодаря goto)
        }

        foreach (BoosterObject boosterObject in availableObjects)                       // Необходимо для рассчётов весов
        {
            combinedWeight += boosterObject.weight;
        }
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
    }

    private void FixedUpdate()
    {
        GenerateObjectsIfRequired();
    }
    void AddObject(float lastObjectX)
    {
        int randomIndex = GenerateIndex();                                                              // Весовое создание коэффициентов
        //1
        // int randomIndex = Random.Range(0, availableObjects.Length);                                 //Переделать индекс под систему вероятности!!!!!!

        //2
        GameObject obj = (GameObject)Instantiate(availableObjects[randomIndex].prefab);

        //3
        float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
        float randomY;
        if (turtle.position.y > 30 && availableObjects[randomIndex].maxY >30)
        {
            randomY = Random.Range(30, availableObjects[randomIndex].maxY);
        }
        else randomY = Random.Range(availableObjects[randomIndex].minY, availableObjects[randomIndex].maxY);
        obj.transform.position = new Vector3(objectPositionX, randomY, 0);


        //5
        objects.Add(obj);
    }

    void GenerateObjectsIfRequired()
    {
        //1
        float playerX = transform.position.x;
        float removeObjectsX = playerX - screenWidthInPoints;
        float addObjectX = playerX + screenWidthInPoints;
        float farthestObjectX = 0;

        //2
        List<GameObject> objectsToRemove = new List<GameObject>();

        foreach (var obj in objects)
        {
            if (obj != null)
            {
                //3
                float objX = obj.transform.position.x;

                //4
                farthestObjectX = Mathf.Max(farthestObjectX, objX);

                //5
                if ((objX < removeObjectsX)/*|| (obj.gameObject.tag == "DestroyThis")*/||(farthestObjectX>addObjectX*1.3))
                    objectsToRemove.Add(obj);
            }

        }

        //6
        foreach (var obj in objectsToRemove)
        {
            objects.Remove(obj);
            Destroy(obj);
        }

        //7
        if (farthestObjectX < addObjectX)
            AddObject(farthestObjectX);
    }

    int GenerateIndex()
    {
        int i=0;                
        float accumulatedWeight=0, randomNumber;
        randomNumber = Random.Range(0, combinedWeight);                                     // Берём случайное число от 0 до max
        foreach (BoosterObject bs in availableObjects)
        {
            accumulatedWeight += bs.weight;                                                 // Накапливаем вес из бустеров
            if (randomNumber < accumulatedWeight)                                           // Если накопленный вес больше случайного числа, то мы нашли что создать
                break;
            i++;  
        }
        return i;
        
    }
}
