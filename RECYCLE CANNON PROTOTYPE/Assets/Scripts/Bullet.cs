using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public MaterialType Type;
    public float Damage = 1f;
    public float Speed = 5f;
    public Vector3 Direction;
    public float Lifespam = 3;

    private void Start()
    {
        Invoke(nameof(AutoDestroy), Lifespam);
    }

    private void FixedUpdate()
    {
        transform.position += Speed * Time.fixedDeltaTime * Direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower")) return;
        if (!other.CompareTag("Enemy"))
        {
            AutoDestroy();
            return;
        }

        Enemy e = other.GetComponent<Enemy>();
        if(Type == MaterialType.Organic)
        {
            if (e.Type == MaterialType.Plastic || e.Type == MaterialType.Metallic) 
                DoDamage(e.GetComponent<Life>());
        }
        else
        {
            if(e.Type == MaterialType.Organic)
                DoDamage(e.GetComponent<Life>());
        }
        AutoDestroy();
    }

    private void AutoDestroy()
    {
        Destroy(this.gameObject);
    }

    private void DoDamage(Life life) => life.Hurt(Damage);
}
