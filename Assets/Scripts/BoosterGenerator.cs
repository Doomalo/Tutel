using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterGenerator : MonoBehaviour
{
    public BoosterObject[] availableObjects;       // Создаваемые префабы
    private float screenWidthInPoints;

    public List<GameObject> objects;            // Созданные на данный момент префабы (нужно для дальнейшего удаления)

    public float objectsMinDistance = 10.0f;
    public float objectsMaxDistance = 20.0f;

    private void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
    }

    private void FixedUpdate()
    {
        GenerateObjectsIfRequired();
    }
    void AddObject(float lastObjectX)
    {
        //1
        int randomIndex = Random.Range(0, availableObjects.Length);                                 //Переделать индекс под систему вероятности!!!!!!

        //2
        GameObject obj = (GameObject)Instantiate(availableObjects[randomIndex].prefab);

        //3
        float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
        float randomY = Random.Range(availableObjects[randomIndex].minY, availableObjects[randomIndex].maxY);
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
            //3
            float objX = obj.transform.position.x;

            //4
            farthestObjectX = Mathf.Max(farthestObjectX, objX);

            //5
            if (objX < removeObjectsX || obj.gameObject.tag == "DestroyThis") 
                objectsToRemove.Add(obj);
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
}
