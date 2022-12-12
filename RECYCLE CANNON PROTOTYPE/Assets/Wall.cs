using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] Life Life;
    public float HealPerHorde;
    private float _curHorde;

    private void Update()
    {
        if (_curHorde != GameManager.Instance.CurrentHorde) Life.CurrentLife += HealPerHorde;
        _curHorde = GameManager.Instance.CurrentHorde;
    }
}
