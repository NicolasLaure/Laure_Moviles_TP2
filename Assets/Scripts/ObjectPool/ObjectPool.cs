using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Transform spawningParent;
        [SerializeField] protected PoolConfigSO poolConfig;
        [SerializeField] private VoidEventChannelSO onBagDespawnedEvent;

        private List<GameObject> _objects;

        public int Count => poolConfig.objectCount;

        protected virtual void Start()
        {
            _objects = new List<GameObject>();

            int configObjectIndex = 0;
            for (int i = 0; i < poolConfig.objectCount; i++)
            {
                if (configObjectIndex >= poolConfig.poolObjects.Count)
                    configObjectIndex = 0;

                GameObject instantiatedObject = GameObject.Instantiate(poolConfig.poolObjects[configObjectIndex].prefab);
                
                if (spawningParent != null)
                    instantiatedObject.transform.parent = spawningParent;
                
                instantiatedObject.SetActive(false);
                _objects.Add(instantiatedObject);
                configObjectIndex++;
            }
        }

        public bool TryGetPooledObject(out GameObject pooledObject)
        {
            pooledObject = null;
            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects[i].activeInHierarchy)
                    continue;

                _objects[i].SetActive(true);
                pooledObject = _objects[i];
                return true;
            }

            return false;
        }

        public bool TryReturnObject(GameObject objectToDisable)
        {
            if (_objects.Contains(objectToDisable))
            {
                foreach (GameObject _object in _objects)
                {
                    if (_object == objectToDisable)
                    {
                        _object.SetActive(false);
                        onBagDespawnedEvent.RaiseEvent();
                        return true;
                    }
                }
            }

            return false;
        }
    }
}