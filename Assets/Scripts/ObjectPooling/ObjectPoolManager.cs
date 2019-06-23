using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolsManager : AbstractSingleton<ObjectPoolsManager> {

    private Dictionary<string, ObjectPool> objectPools;
    private Dictionary<string, Transform> poolParents;

    public bool TryPool(GameObject obj) {
        return GetPool(obj).TryPool(obj);
    }

    public GameObject Unpool(GameObject obj) {
        GameObject unpooled = GetPool(obj).Unpool();
        if (!unpooled.transform.parent) {
            SetParent(unpooled);
        }
        return unpooled;
    }

    public void SetParent(GameObject obj) {
        Transform parent;
        if (poolParents == null)
            poolParents = new Dictionary<string, Transform>();
        try {
            parent = poolParents[obj.name];
        }
        catch (KeyNotFoundException) {
            parent = new GameObject(obj.name).transform;
            poolParents.Add(obj.name, parent);
            parent.parent = transform;
        }
        obj.transform.parent = parent;
    }

    private ObjectPool GetPool(GameObject obj) {
        ObjectPool pool;
        if (objectPools == null)
            objectPools = new Dictionary<string, ObjectPool>();
        try {
            pool = objectPools[obj.name];
        }
        catch (KeyNotFoundException) {
            pool = new ObjectPool(obj);
            objectPools.Add(obj.name, pool);
        }
        return pool;

    }

}