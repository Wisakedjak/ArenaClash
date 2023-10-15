using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

namespace Managers
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static List<PooledObjectInfo> ObjectPools=new List<PooledObjectInfo>();
        
        public static PoolTypeEnum PoolingType;

        private GameObject _objectPoolEmptyHolder;
        
        private static GameObject _particleSystemsHolder;
        private static GameObject _gameObjectsHolder;

        private void Awake()
        {
            SetupHolders();
        }

        private void SetupHolders()
        {
            _objectPoolEmptyHolder = new GameObject("ObjectPoolHolder");
            
            _particleSystemsHolder = new GameObject("ParticleSystemsHolder");
            _particleSystemsHolder.transform.SetParent(_objectPoolEmptyHolder.transform);
            
            _gameObjectsHolder = new GameObject("GameObjectsHolder");
            _gameObjectsHolder.transform.SetParent(_objectPoolEmptyHolder.transform);
        }

        public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolTypeEnum poolingType)
        {
            PooledObjectInfo pool=ObjectPools.Find(x=>x.LookUpString==objectToSpawn.name);

            if (pool==null)
            {
                pool=new PooledObjectInfo(){LookUpString = objectToSpawn.name};
                ObjectPools.Add(pool);
            }

            GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

            if (spawnableObj==null)
            {
                GameObject holder = ReturnHolderObject(poolingType);
                spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
                if (holder != null)
                {
                    spawnableObj.transform.SetParent(holder.transform);
                }
            }
            else
            {
                spawnableObj.transform.position = spawnPosition;
                spawnableObj.transform.rotation = spawnRotation;
                spawnableObj.SetActive(true);
                pool.InactiveObjects.Remove(spawnableObj);
            }
            
            return spawnableObj;

        }
        public static GameObject SpawnObject(GameObject objectToSpawn, Transform parentTransform)
        {
            PooledObjectInfo pool=ObjectPools.Find(x=>x.LookUpString==objectToSpawn.name);

            if (pool==null)
            {
                pool=new PooledObjectInfo(){LookUpString = objectToSpawn.name};
                ObjectPools.Add(pool);
            }

            GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

            if (spawnableObj==null)
            {
                spawnableObj = Instantiate(objectToSpawn, parentTransform);
            }
            else
            {
                spawnableObj.SetActive(true);
                pool.InactiveObjects.Remove(spawnableObj);
            }
            
            return spawnableObj;

        }

        public static void ReturnObjectToPool(GameObject obj)
        {
            string goName = obj.name.Substring(0, obj.name.Length - 7);
            
            PooledObjectInfo pool=ObjectPools.Find(x=>x.LookUpString==goName);

            if (pool==null)
            {
                Debug.LogError("No pool exists for this object");
            }
            else
            {
                obj.SetActive(false);
                pool.InactiveObjects.Add(obj);
            }
        }
        
        private static GameObject ReturnHolderObject(PoolTypeEnum poolType)
        {
            switch (poolType)
            {
                case PoolTypeEnum.ParticleSystem:
                    return _particleSystemsHolder;
                case PoolTypeEnum.GameObject:
                    return _gameObjectsHolder;
                case PoolTypeEnum.None:
                    return null;
                default:
                    return null;
            }
        }
        
    }

    public class PooledObjectInfo
    {
        public string LookUpString;
        public List<GameObject> InactiveObjects=new List<GameObject>();
        
    }
}