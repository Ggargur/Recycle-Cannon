using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    public Transform TrashHolder;
    public MaterialType Type;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (_playerController.CurrentTrash == null || _playerController.CurrentTrash.Type != Type) return;
        _playerController.DroppableArea = TrashHolder;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _playerController.DroppableArea = null;
    }
}
