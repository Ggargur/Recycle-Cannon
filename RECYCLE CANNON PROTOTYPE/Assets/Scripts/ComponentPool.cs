using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPool<T> where T : MonoBehaviour
{
    private List<MonoBehaviour> pool = new List<MonoBehaviour>();
    public int Count
    {
        get => pool.Count;
    }

    public void Add(MonoBehaviour mono)
    {
        pool.Add(Object.Instantiate(mono));
    }

    public GameObject Get()
    {
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeSelf)
            {
                mono.gameObject.SetActive(true);
                return mono.gameObject;
            }
        }
        return null;
    }

    public GameObject Get<U>() where U : MonoBehaviour, T
    {
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeSelf && mono.GetComponent<U>() != null)
            {
                mono.gameObject.SetActive(true);
                return mono.gameObject;
            }
        }
        return null;
    }

    public void Stash(MonoBehaviour monoBehaviour)
    {
        foreach (var mono in pool)
        {
            if (mono == monoBehaviour) mono.gameObject.SetActive(false);
        }
    }
}
