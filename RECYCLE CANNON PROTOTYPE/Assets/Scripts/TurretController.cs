using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject PlasticShot;
    [SerializeField] GameObject MetallicShot;
    [SerializeField] GameObject OrganicShot;

    [Header("References")]
    public Transform TrashHolder;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _joystick;
    public Trash Ammo;

    [Header("Values")]
    [SerializeField] Vector3 Offset;
    public float Speed;
    private float _direction;
    private float _rotationY;
    private Player.TouchActions _touchAction;

    void Start()
    {
        _touchAction = GameManager.Instance.PlayerController.Touch;
        _touchAction.TurretDirectional.performed += GetDirection;
        _touchAction.TurretDirectional.canceled += ctx => _direction = 0;
    }

    private void GetDirection(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>().x;
    }

    void FixedUpdate()
    {
        _rotationY += _direction * Speed * Time.fixedDeltaTime;
        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);
        transform.localEulerAngles = new Vector3(0, _rotationY);
    }

    public void Shoot()
    {
        if (Ammo == null) return;

        var sh = Instantiate(GetBulletTypeFromAmmo());
        Vector3 realOffset = transform.forward * Offset.x + transform.up * Offset.y + transform.right * Offset.z;
        sh.transform.position = transform.position + realOffset;
        sh.GetComponent<Bullet>().Direction = transform.forward;
        Ammo.Size--;
        if (Ammo.Size <= 0) Destroy(Ammo.gameObject);
    }

    private GameObject GetBulletTypeFromAmmo() => Ammo.Type switch
    {
        MaterialType.Metallic => MetallicShot,
        MaterialType.Plastic => PlasticShot,
        MaterialType.Organic => OrganicShot,
        _ => null
    };
}
