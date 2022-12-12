using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DropPool : ScriptableObject
{
    [System.Serializable]
    private class Drop
    {
        public float Percentage;
        public GameObject DroppedObject;
    }

    [SerializeField] private List<Drop> _drops = new();

    public GameObject GetRandomDrop() => _drops[Random.Range(0, _drops.Count)].DroppedObject;

    public GameObject GetDrop(int index) => _drops[index].DroppedObject;

    public GameObject GetRandomDropWithPercentages()
    {
        float value = Random.value * 100f;
        float count = 0f;
        foreach(var drop in _drops)
        {
            if(count < value && value < count + drop.Percentage)
            {
                return drop.DroppedObject;
            }
            count += drop.Percentage;
        }
        return null;
    }
}
