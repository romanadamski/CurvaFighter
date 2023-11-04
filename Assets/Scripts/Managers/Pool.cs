using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Pool
{
    public int StartPoolCount;
    public BasePoolableController PoolObjectPrefab;
    public bool CanGrow;
    public Queue<BasePoolableController> PooledObjects = new Queue<BasePoolableController>();
    public Transform Parent;
    public string PoolableNameType => PoolObjectPrefab.GetComponent<BasePoolableController>().PoolableType;

    [HideInInspector]
    public int ObjectCount;

    /// <summary>
    /// Type choosen by attached Poolable component
    /// </summary>
    public Type PoolableComponentType => PoolObjectPrefab.GetType();

    [HideInInspector]
    public List<BasePoolableController> ObjectsOutsidePool = new List<BasePoolableController>();

    public void ReturnAllToPool()
    {
        foreach (var poolObject in ObjectsOutsidePool.ToList())
        {
            ReturnToPool(poolObject);
        }
        ObjectsOutsidePool.Clear();
    }

    public void ReturnToPool(BasePoolableController objectToReturn)
    {
        if (!PooledObjects.Contains(objectToReturn))
        {
            objectToReturn.gameObject.SetActive(false);
            PooledObjects.Enqueue(objectToReturn);
        }
    }
}