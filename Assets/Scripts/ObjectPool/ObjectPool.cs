using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ObjectPool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Transform spawningParent;
        [SerializeField] protected PoolConfigSO poolConfig;

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

                GameObject instantiatedObject = Instantiate(poolConfig.poolObjects[configObjectIndex].prefab);

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

        public GameObject GetRandomPooledObject()
        {
            int randomIndex = 0;
            List<int> checkedIndices = new List<int>();

            while (checkedIndices.Count < _objects.Count)
            {
                randomIndex = Random.Range(0, _objects.Count);
                if (!_objects[randomIndex].activeInHierarchy)
                {
                    _objects[randomIndex].SetActive(true);   
                    return _objects[randomIndex];
                }

                if (!checkedIndices.Contains(randomIndex))
                    checkedIndices.Add(randomIndex);
            }

            return null;
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
                        return true;
                    }
                }
            }

            return false;
        }
    }
}