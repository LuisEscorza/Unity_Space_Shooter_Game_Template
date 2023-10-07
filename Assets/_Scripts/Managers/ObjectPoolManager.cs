using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolManager : MonoBehaviour
{
    #region Fields
    [field: Header("Components")]
    public static ObjectPoolManager Manager { get; private set; }
    private GameObject _objectPoolEmptyHolder;
    public enum PoolType { None, PlayerProjectile, EnemyProjectile, EnemyShip, Pickup }
    private readonly Dictionary<PoolType, GameObject> _parentObjects = new();
    private readonly Dictionary<string, PooledObjectInfo> ObjectPools = new();
    #endregion

    private void Awake()
    {
        if (Manager != null && Manager != this)
            Destroy(this);
        else
            Manager = this;
        DontDestroyOnLoad(gameObject);

        SetupEmpties();
    }

    private void SetupEmpties()
    {
        _objectPoolEmptyHolder = new GameObject("PooledObjects");

        foreach (PoolType poolType in PoolType.GetValues(typeof(PoolType)))
        {
            if (poolType == PoolType.None) continue;

            GameObject emptyObject = new(poolType.ToString());
            emptyObject.transform.SetParent(_objectPoolEmptyHolder.transform);
            _parentObjects[poolType] = emptyObject;
        }
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name[..^7];

        if (!ObjectPools.TryGetValue(goName, out PooledObjectInfo pool))
            Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }
    
    public GameObject SpawnObject(GameObject objectToSpawn, Vector2 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        if (!ObjectPools.TryGetValue(objectToSpawn.name, out PooledObjectInfo pool))
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(objectToSpawn.name, pool);
        }

        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

        if (spawnableObj == null)
        {
            GameObject parentObject = SetParentObject(poolType);
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            if (parentObject != null)
                spawnableObj.transform.SetParent(parentObject.transform);
        }
        else
        {
            spawnableObj.transform.SetPositionAndRotation(spawnPosition, spawnRotation);
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }
        return spawnableObj;
    }
    
    private GameObject SetParentObject(PoolType poolType)
    {
        if (_parentObjects.TryGetValue(poolType, out GameObject parentObject))
        {
            return parentObject;
        }
        return null;
    }

    public class PooledObjectInfo
    {
        public string LookupString;
        public List<GameObject> InactiveObjects = new();
    }

}
