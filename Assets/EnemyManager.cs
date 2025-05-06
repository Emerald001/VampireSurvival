using System.Threading.Tasks;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    private async void StartSpawning()
    {
        while (true)
        {
            await Task.Delay(2000); // Wait for 2 seconds
            SpawnEnemy();

            if (GlobalNumerals.SpawnEnemies == false)
            {
                Debug.Log("Enemy Spawning Stopped");
                break; // Exit the loop if spawning is disabled
            }
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 mapSize = GameManager.Instance.MapSize; // MapSize is a Vector2
        Vector3 playerPosition = GameManager.Instance.Player.transform.position;
        float safeZoneRadius = 5f; // Define the radius of the safe zone around the player

        Vector3 randomPosition;
        do
        {
            // Generate a random position within the map boundaries
            randomPosition = new Vector3(
                Random.Range(-mapSize.x / 2, mapSize.x / 2), // Use x for horizontal boundaries
                0,
                Random.Range(-mapSize.y / 2, mapSize.y / 2) // Use y for vertical boundaries
            );
        }
        while (Vector3.Distance(randomPosition, playerPosition) < safeZoneRadius);

        return randomPosition;
    }
}
