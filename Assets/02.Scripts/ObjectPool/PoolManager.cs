using MonsterLove.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    public bool logStatus;
    public Transform root;

    private Dictionary<GameObject, ObjectPool<GameObject>> prefabLookup;
    private Dictionary<GameObject, ObjectPool<GameObject>> instanceLookup;

    private bool dirty = false;

    void Awake()
    {
        prefabLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();
        instanceLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();
    }


    public void warmPool(GameObject prefab, int size)
    {
        if (prefab == null)
            return;

        if (prefabLookup.ContainsKey(prefab))
        {
            throw new Exception("Pool for prefab " + prefab.name + " has already been created");
        }
        var pool = new ObjectPool<GameObject>(() =>
        {
            GameObject item = InstantiatePrefab(prefab);
            item.SetActive(false);
            return item;

        }, size);
        prefabLookup[prefab] = pool;

        dirty = true;

        if (logStatus)
            Debug.Log("Pool Warm : " + prefab.name);
    }

    public GameObject spawnObject(GameObject prefab)
    {
        return spawnObject(prefab, Vector3.zero, Quaternion.identity);
    }

    public GameObject spawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!prefabLookup.ContainsKey(prefab))
        {
            WarmPool(prefab, 1);
        }

        var pool = prefabLookup[prefab];

        var clone = pool.GetItem();
        clone.transform.position = position;
        clone.transform.rotation = rotation;
        clone.SetActive(true);

        instanceLookup.Add(clone, pool);
        dirty = true;

        if (logStatus)
            Debug.Log("Pool Spawn : " + prefab.name);

        return clone;
    }

    public void releaseObject(GameObject clone)
    {
        clone.SetActive(false);

        if (instanceLookup.ContainsKey(clone))
        {
            instanceLookup[clone].ReleaseItem(clone);
            if (root != null) clone.transform.SetParent(root);
            clone.gameObject.SetActive(false);
            instanceLookup.Remove(clone);
            dirty = true;

            if (logStatus)
                Debug.Log("Pool Release : " + clone.name);
        }
        else
        {
            Debug.LogWarning("No pool contains the object: " + clone.name);
        }
    }


    private GameObject InstantiatePrefab(GameObject prefab)
    {
        var go = Instantiate(prefab) as GameObject;
        if (root != null) go.transform.SetParent(root);
        return go;
    }

    public void PrintStatus()
    {
        foreach (KeyValuePair<GameObject, ObjectPool<GameObject>> keyVal in prefabLookup)
        {
            Debug.Log(string.Format("Object Pool for Prefab: {0} In Use: {1} Total {2}", keyVal.Key.name, keyVal.Value.CountUsedItems, keyVal.Value.Count));
        }
    }

    #region Static API

    public static void WarmPool(GameObject prefab, int size)
    {
        Instance.warmPool(prefab, size);
    }

    public static GameObject SpawnObject(GameObject prefab)
    {
        return Instance.spawnObject(prefab);
    }

    public static GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return Instance.spawnObject(prefab, position, rotation);
    }

    public static void ReleaseObject(GameObject clone)
    {
        Instance.releaseObject(clone);
    }

    #endregion
}


