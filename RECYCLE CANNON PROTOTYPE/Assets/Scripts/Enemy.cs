using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : WalkingEntity
{
    [Header("References")]
    public  Transform PlayerTarget;
    public Transform WallTarget;
    public float Damage = 1f;
    public float Cooldown = 1f;
    [SerializeField] private ParticleSystem _deathParticle;
    [SerializeField] private DropPool _dropPool;
    public abstract MaterialType Type { get;}

    List<Life> Collisions = new List<Life>();

    void Start()
    {
        PlayerTarget = GameManager.Instance.Player;
        WallTarget = GameManager.Instance.Wall;
        Destination = PlayerTarget.position;
        _navAgent.destination = Destination;
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(DoDamageToAll),Cooldown,Cooldown);
    }

    private new void FixedUpdate()
    {
        var playerDistance = Vector3.Distance(PlayerTarget.position, transform.position);
        var WallDistance = Vector3.Distance(WallTarget.position, transform.position);
        if (playerDistance < WallDistance)
            Destination = PlayerTarget.position;
        else
            Destination = WallTarget.position;

        base.FixedUpdate();

        if (Collisions.Count <= 0) return;
    }

    public void Explode()
    {
        Instantiate(_dropPool.GetRandomDrop()).transform.position = transform.position;
        Instantiate(_deathParticle).transform.position = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Life life = collision.gameObject.GetComponent<Life>();
        if (life == null) return;

        life.Hurt(Damage);
        if(!Collisions.Contains(life)) Collisions.Add(life);
    }


    private void OnCollisionExit(Collision collision)
    {
        Life life = collision.gameObject.GetComponent<Life>();
        if (life == null) return;

        Collisions.Remove(life);
    }

    private void DoDamageToAll()
    {
        foreach (var lv in Collisions)
            lv.Hurt(Damage);
    }
}
