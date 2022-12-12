using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MaterialType
{
    Plastic,
    Organic,
    Metallic
}

[CreateAssetMenu]
public class EnemyPool : ScriptableObject
{
    


    public List<GameObject> PlasticEnemies = new List<GameObject>();
    public List<GameObject> OrganicEnemies = new List<GameObject>();
    public List<GameObject> MetallicEnemies = new List<GameObject>();

    public GameObject GetEnemy(MaterialType type)
    {
        if (type == MaterialType.Plastic) return PlasticEnemies[Random.Range(0, PlasticEnemies.Count)];
        if (type == MaterialType.Organic) return OrganicEnemies[Random.Range(0, OrganicEnemies.Count)];
        if (type == MaterialType.Metallic) return MetallicEnemies[Random.Range(0, MetallicEnemies.Count)];
        return null; 
    }
}
