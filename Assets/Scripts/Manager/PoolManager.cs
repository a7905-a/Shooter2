using System.Collections.Generic;
using UnityEngine;

namespace ProjectTwo.Manager
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager instance;

        [Header("풀링할 프리팹과 풀 크기")]
        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private int poolSize = 10;

        private List<GameObject>[] pooledObjects;


        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            InitObjectPool();
        }

        private void InitObjectPool()
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
                    obj.transform.SetParent(this.transform);
                    
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
}

        

