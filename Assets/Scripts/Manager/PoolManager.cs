using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    [SerializeField] GameObject[] prefabs;
    List<GameObject>[] pooledObjects;
    [SerializeField] int poolSize = 1;



    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        InitObjectPool();
    }

    void InitObjectPool()
    {
        pooledObjects = new List<GameObject>[prefabs.Length];
        GameObject obj = null;
        for (int i = 0; i < prefabs.Length; i++)
        {
            pooledObjects[i] = new List<GameObject>();
            for (int j = 0; j < poolSize; j++)
            {
                obj = Instantiate(prefabs[i]);
                obj.SetActive(false);
                pooledObjects[i].Add(obj);
            }
        }
    }

    public GameObject ActivateObject(int index)
    {
        GameObject obj = null;

        for (int i = 0; i < pooledObjects[index].Count; i++)
        {
            if (!pooledObjects[index][i].activeInHierarchy)
            {
                obj = pooledObjects[index][i];
                obj.SetActive(true);
                return obj;
            }
        }
        obj = Instantiate(prefabs[index]);
        pooledObjects[index].Add(obj);
        obj.SetActive(true);

        return obj;
    }

    public void SetPosition(GameObject obj, Vector3 position)
    {
        obj.transform.position = position;
    }

    public void DeactivateObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
        

