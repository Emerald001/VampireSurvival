using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private EnemyConfig config;

    public static List<Enemy> spawnedEnemies { get; private set; } = new List<Enemy>();

    public static EnemyManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public async void StartSpawning()
    {
        while (true)
        {
            await Task.Delay(2000); // Wait for 2 seconds

            if (GlobalNumerals.SpawnEnemies == false)
            {
                Debug.Log("Enemy Spawning Stopped");
                break; // Exit the loop if spawning is disabled
            }

            SpawnEnemy();
        }
    }

    public void DoKnockback(float strength, Vector2 origin)
    {
        foreach (var enemy in spawnedEnemies)
        {
            var direction = (enemy.transform.position - (Vector3)origin).normalized;
            var dis = Vector3.Distance(enemy.transform.position, origin);

            if (dis < 10)
                enemy.TakeKnockback(direction, (strength - dis) / 2);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.SetData(config);

        spawnedEnemies.Add(enemy);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 mapSize = GameManager.Instance.CurrentArea.mapSize; // MapSize is a Vector2
        Vector3 playerPosition = GameManager.Instance.Player.transform.position;

        float maxDistance = 20f;
        float safeZoneRadius = 5f; // Define the radius of the safe zone around the player
        float padding = 0.6f;

        Vector3 randomPosition;
        do
        {
            // Generate a random position within the map boundaries
            randomPosition = new Vector3(
                Random.Range(-mapSize.x / 2 + padding, mapSize.x / 2 - padding), // Use x for horizontal boundaries
                Random.Range(-mapSize.y / 2 + padding, mapSize.y / 2 - padding) // Use y for vertical boundaries
            );
        }
        while (Vector3.Distance(randomPosition, playerPosition) < safeZoneRadius || Vector3.Distance(randomPosition, playerPosition) > maxDistance);

        return randomPosition;
    }
}
