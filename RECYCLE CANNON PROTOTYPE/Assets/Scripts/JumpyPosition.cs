using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyPosition : MonoBehaviour
{
    [SerializeField] private float _frequency;
    [SerializeField] private float _amplitude;

    [Space(10)]
    [SerializeField] private bool _moveOnX;
    [SerializeField] private bool _moveOnY;
    [SerializeField] private bool _moveOnZ;

    public bool Enabled = true;
    private Vector3 _defaultLocalRotation;

    private void Start()
    {
        _defaultLocalRotation = transform.localPosition;
    }

    void Update()
    {
        if (!Enabled) return;

        var x = _moveOnX ? Mathf.Sin(Time.time * _frequency) * _amplitude : 0;
        var y = _moveOnY ? Mathf.Cos(Time.time * _frequency) * _amplitude : 0;
        var z = _moveOnZ ? Mathf.Tan(Time.time * _frequency) * _amplitude : 0;

        transform.localPosition = _defaultLocalRotation + new Vector3(x, y, z);
    }

    public void Stop() => Enabled = false;
}
