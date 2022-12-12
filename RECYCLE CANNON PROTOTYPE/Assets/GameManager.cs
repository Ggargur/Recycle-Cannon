using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player PlayerController;

    public Transform Wall;
    public Transform Player;
    public int CurrentHorde;
    public bool IsGameOn;

    [Header("Enemy Position Range")]
    [SerializeField] Vector3 _spawnPosition;
    [SerializeField] float _spawnRange;
    [Header("Enemy Spawn Cooldown")]
    public float SpawnCooldown = 10;
    [SerializeField] float _minSpawnCooldown = 1f;
    public float TimeDificultyScaler = 0.5f;
    [Header("Enemy Horde Size")]
    public float HordeSize = 1;
    public int NumberDificultyScaler = 1;
    [Header("Enemy Spawn Chances")]
    [SerializeField] private float _metallicSpawnChance = 0.2f;
    [SerializeField] private float _organicSpawnChance = 0.2f;
    [SerializeField] private float _plasticSpawnChance = 0.6f;

    private int _spawnedCounter;
    ComponentPool<Enemy> Enemies = new ComponentPool<Enemy>();

    [Header("References")]
    [SerializeField] private EnemyPool Pool;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(this);

        PlayerController = new Player();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void OnEnable()
    {
        PlayerController.Enable();
    }

    private void OnDisable()
    {
        PlayerController.Disable();
    }
    private void SpawnMetallic() => SpawnEnemy(MaterialType.Metallic);
    private void SpawnPlastic() => SpawnEnemy(MaterialType.Plastic);
    private void SpawnOrganic() => SpawnEnemy(MaterialType.Organic);
    public void SpawnEnemy(MaterialType type)
    {
        _spawnedCounter++;
        GameObject newenemy = null;
        switch (type)
        {
            case MaterialType.Plastic:
                newenemy = Enemies.Get<PlasticEnemy>();
                break;
            case MaterialType.Organic:
                newenemy = Enemies.Get<OrganicEnemy>();
                break;
            case MaterialType.Metallic:
                newenemy = Enemies.Get<MetallicEnemy>();
                break;
        }

        if (newenemy == null)
        {
            var n = MakeNewEnemy(type);
            Enemies.Add(n);
            n.PlayerTarget = Player;
            n.WallTarget = Wall;
            newenemy = n.gameObject;
        }
        AdjustPosition(newenemy);
    }
    private Enemy MakeNewEnemy(MaterialType type)
    {
        Enemy newEnemyMono = null;
        switch (type)
        {
            case MaterialType.Plastic:
                newEnemyMono = (Pool.PlasticEnemies[Random.Range(0, Pool.PlasticEnemies.Count)].GetComponent<Enemy>());
                break;
            case MaterialType.Organic:
                newEnemyMono = (Pool.OrganicEnemies[Random.Range(0, Pool.OrganicEnemies.Count)].GetComponent<Enemy>());
                break;
            case MaterialType.Metallic:
                newEnemyMono = (Pool.MetallicEnemies[Random.Range(0, Pool.MetallicEnemies.Count)].GetComponent<Enemy>());
                break;
        }
        return newEnemyMono;
    }

    private void AdjustPosition(GameObject enemy)
    {
        enemy.transform.position = _spawnPosition + new Vector3(Random.Range(-_spawnRange, _spawnRange), 0, Random.Range(0, _spawnRange));
    }

    public void KillEntity(GameObject gameObject)
    {
        if (gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is dead");
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            var e = gameObject.GetComponent<Enemy>();
            Enemies.Stash(e);
            e.Explode();
        }
        else
        {
            Debug.Log($"Object {gameObject.name} is dead");
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Mathf.Clamp(SpawnCooldown - TimeDificultyScaler * CurrentHorde, _minSpawnCooldown, SpawnCooldown));
            if (_spawnedCounter >= HordeSize + NumberDificultyScaler * CurrentHorde) GoToNextHorde();

            float value = Random.value;
            if (value < _organicSpawnChance)
            {
                SpawnOrganic();
                continue;
            }
            if (value < _organicSpawnChance + _metallicSpawnChance)
            {
                SpawnMetallic();
                continue;
            }
            if (value < _organicSpawnChance + _metallicSpawnChance + _plasticSpawnChance)
            {
                SpawnPlastic();
                continue;
            }
        }
    }

    private void GoToNextHorde()
    {
        CurrentHorde++;
        _spawnedCounter = 0;
    }
}
