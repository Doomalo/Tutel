using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterGenerator : MonoBehaviour
{
    public List<BoosterObject> availableObjects;       // ����������� �������

    public BoosterObject[] level2;                      // ��������� ������� �� �������!!!
    public BoosterObject[] level3;
    public BoosterObject[] level4;

    private float screenWidthInPoints;

    public List<GameObject> objects;            // ��������� �� ������ ������ ������� (����� ��� ����������� ��������)
    public Transform turtle;
    public float objectsMinDistance = 10.0f;
    public float objectsMaxDistance = 20.0f;

    private float combinedWeight;

    private int level;

    private void Start()
    {
        level = PlayerPrefs.GetInt("Level", 1);                                                                 // �������� �������, �� ������� � ������ ������ ��������� �������
        switch(level)
        {
            case 4: foreach (BoosterObject bo in level4) availableObjects.Add(bo);goto case 3;                  // ���� 4-�� �������, � ������ ��������� �������� ��������� ������� 4-��� ������
            case 3: foreach (BoosterObject bo in level3) availableObjects.Add(bo);goto case 2;                  // � 2-�� � 3-�� �� �� �����
            case 2: foreach (BoosterObject bo in level2) availableObjects.Add(bo); break;                       // ����� ����������� ��� ������� ������ ������� (���������� goto)
        }

        foreach (BoosterObject boosterObject in availableObjects)                       // ���������� ��� ��������� �����
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
        int randomIndex = GenerateIndex();                                                              // ������� �������� �������������
        //1
        // int randomIndex = Random.Range(0, availableObjects.Length);                                 //���������� ������ ��� ������� �����������!!!!!!

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
        randomNumber = Random.Range(0, combinedWeight);                                     // ���� ��������� ����� �� 0 �� max
        foreach (BoosterObject bs in availableObjects)
        {
            accumulatedWeight += bs.weight;                                                 // ����������� ��� �� ��������
            if (randomNumber < accumulatedWeight)                                           // ���� ����������� ��� ������ ���������� �����, �� �� ����� ��� �������
                break;
            i++;  
        }
        return i;
        
    }
}
