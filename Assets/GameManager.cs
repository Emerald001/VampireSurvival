using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player playerPrefab;
    [SerializeField] private CameraFollow camFollow;
    [SerializeField] private EnemyManager enemyManager;

    [SerializeField] private Vector2 mapSize;
    [SerializeField] private Transform map;

    public Player Player { get; private set; }
    public Vector2 MapSize { get => mapSize; set => mapSize = value; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
        GlobalNumerals.SpawnEnemies = true;
    }

    private void OnDisable()
    {
        GlobalNumerals.SpawnEnemies = false;
    }

    private void Start()
    {
        map.localScale = new Vector3(mapSize.x / 10, 1, mapSize.y / 10);
        map.GetComponent<Renderer>().material.mainTextureScale = new Vector2(mapSize.x, mapSize.y);

        SpawnPlayer();

        //enemyManager.StartSpawning();
    }

    private void SpawnPlayer()
    {
        Player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        camFollow.SetFollower(Player.gameObject);
    }
}
