using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool
{
    private List<GameObject> pool = new List<GameObject>();
    public GameObject Prefab;
    public int Count
    {
        get => pool.Count;
    }

    public GameObjectPool(GameObject prefab)
    {
        Prefab = prefab;
    }
    public GameObjectPool()
    {
    }
    public void Add()
    {
        if(Prefab != null)
        {
            pool.Add(Prefab);
            return;
        }
        pool.Add(new GameObject());
    }

    public void Add(GameObject prefab)
    {
        pool.Add(Object.Instantiate(prefab));
    }

    public GameObject Get()
    {
        foreach (var go in pool)
        {
            if (!go.activeSelf)
            {
                go.SetActive(true);
                return go;
            }
        }
        if (Prefab == null)
        {
            var newgo = new GameObject();
            return newgo;
        }
        return null;
    }

    public GameObject GetNext()
    {
        foreach(var go in pool)
        {
            if (!go.activeSelf)
            {
                go.SetActive(true);
                return go;
            }
        }
        if(Prefab == null)
        {
            var newgo = new GameObject();
            return newgo;
        }
        return Object.Instantiate(Prefab);
    }

    public void Stash(GameObject gameObject)
    {
        foreach (var go in pool)
        {
            if (go == gameObject) go.SetActive(false);
        }
    }
}
