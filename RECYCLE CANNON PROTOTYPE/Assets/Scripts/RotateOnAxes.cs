using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnAxes : MonoBehaviour
{
    [SerializeField] private float _frequency;
    [SerializeField] private float _amplitude;

    [Space(10)]
    [SerializeField] private bool _rotateOnX;
    [SerializeField] private bool _rotateOnY;
    [SerializeField] private bool _rotateOnZ;

    [Space(5)]
    [SerializeField] private bool _backAndForth;

    private Vector3 _defaultLocalRotation;
    private float x, y, z;
    public bool Enabled = true;


    private void Start()
    {
        _defaultLocalRotation = transform.localEulerAngles;
    }

    void Update()
    {
        if (!Enabled) return;

        if (_backAndForth)
        {
            x = _rotateOnX ? Mathf.Sin(Time.time * _frequency) * _amplitude : 0;
            y = _rotateOnY ? Mathf.Cos(Time.time * _frequency) * _amplitude : 0;
            z = _rotateOnZ ? Mathf.Tan(Time.time * _frequency) * _amplitude : 0;
        }
        else
        {
            x += _rotateOnX ? Time.deltaTime * (_frequency) : 0;
            y += _rotateOnY ? Time.deltaTime * (_frequency) : 0;
            z += _rotateOnZ ? Time.deltaTime * (_frequency) : 0;
        }

        transform.localEulerAngles = _defaultLocalRotation + new Vector3(x, y, z);
    }

    public void Stop() => Enabled = false;
}
