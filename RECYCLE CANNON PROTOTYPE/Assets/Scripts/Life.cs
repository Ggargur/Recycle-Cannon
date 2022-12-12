using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public float MaxLife;
    public float CurrentLife;

    private void Update()
    {
        if (CurrentLife > MaxLife) CurrentLife = MaxLife;
        if (CurrentLife <= 0) Die();
    }

    public void Hurt(float amount) => CurrentLife -= amount;
    public void Heal(float amount) => CurrentLife += amount;

    public void Die()
    {
        GameManager.Instance.KillEntity(gameObject);
    }

#if UNITY_EDITOR
    private void OnMouseDown()
    {
        Die();
    }
#endif
}
