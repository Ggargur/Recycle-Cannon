using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : WalkingEntity
{
    public Vector2 Direction;
    public Transform DroppableArea;

    private Trash _currentTrash;
    public Trash CurrentTrash
    {
        get => _currentTrash;
        set
        {
            _animator.SetBool("IsHolding", value != null);
            if (value != null)
            {
                value.transform.SetParent(_trashSpace);
            }
            _currentTrash = value;
        }
    }
    public bool IsHoldingTrash
    {
        get => CurrentTrash != null;
    }

    [Header("References")]
    [SerializeField] private TurretController _turret;
    [SerializeField] private  Transform _trashSpace;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        if(_animator == null) _animator = GetComponent<Animator>();
        GameManager.Instance.PlayerController.Touch.PlayerDirectional.performed += GetDirection;
        GameManager.Instance.PlayerController.Touch.PlayerDirectional.canceled += ctx => Direction = Vector2.zero;
    }

    private void GetDirection(InputAction.CallbackContext context) => Direction = context.ReadValue<Vector2>();

    private new void FixedUpdate()
    {
        Destination = transform.position + new Vector3(Direction.x, 0, Direction.y);
        _animator.SetBool("IsMoving", Direction.sqrMagnitude > 0.01f);
        base.FixedUpdate();
    }

    public void DropTrash()
    {
        if (DroppableArea == null || CurrentTrash == null) return;

        _turret.Ammo = CurrentTrash;
        CurrentTrash.transform.SetParent(DroppableArea);
        CurrentTrash.GoToPosition();
        CurrentTrash = null;
    }
}
