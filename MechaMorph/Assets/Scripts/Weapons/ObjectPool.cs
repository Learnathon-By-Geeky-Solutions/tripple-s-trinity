using System.Collections.Generic;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool instance;
        private List<GameObject> _pooledObjects = new List<GameObject>();
        private int _amountToPool = 20;
        [SerializeField] private GameObject bulletPrefab;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        void Start()
        {
            for (int i = 0; i < _amountToPool; i++)
            {
                GameObject obj = Instantiate(bulletPrefab);
                obj.SetActive(false);
                _pooledObjects.Add(obj);
            }
        
        }

        public GameObject GetPooledObjects()
        {
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                if (!_pooledObjects[i].activeInHierarchy)
                {
                    return _pooledObjects[i];
                }
                
            }

            return null;
        }

    }
}
