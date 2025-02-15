
using System.Collections.Generic;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool instance;
        [SerializeField] private List<GameObject> poolObjects = new List<GameObject>();
        [SerializeField] private int amountOfPoolObjects = 20;
        [SerializeField] private GameObject meeleWeapon;
        [SerializeField] private GameObject droneWeapon;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            for (int i = 0; i < amountOfPoolObjects; i++)
            {
                GameObject obj1 = Instantiate(meeleWeapon);
                obj1.SetActive(false);
                poolObjects.Add(obj1);

                GameObject obj2 = Instantiate(droneWeapon);
                obj2.SetActive(false);
                poolObjects.Add(obj2);  
            }
        }

        public GameObject GetPooledObject()
        {
            foreach (GameObject obj in poolObjects)
            {
                if (!obj.activeInHierarchy)
                {
                    return obj;
                }
            }

            // If no object is available, optionally instantiate a new one (optional)
            GameObject newObj = Instantiate(meeleWeapon);
            newObj.SetActive(false);
            poolObjects.Add(newObj);
            return newObj;
        }


    }
}