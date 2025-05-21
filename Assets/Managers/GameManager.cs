using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player playerPrefab;
    [SerializeField] private CameraFollow camFollow;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private ExpierenceManager ExpManager;

    [SerializeField] private List<WeaponConfig> weapons;

    [SerializeField] private Vector2 mapSize;
    [SerializeField] private Transform map;

    public Player Player { get; private set; }
    public Vector2 MapSize { get => mapSize; set => mapSize = value; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnDisable()
    {
        GlobalNumerals.SpawnEnemies = false;
    }

    public void StartGame()
    {
        map.localScale = new Vector3(mapSize.x / 10, 1, mapSize.y / 10);
        map.GetComponent<Renderer>().material.mainTextureScale = new Vector2(mapSize.x, mapSize.y);

        Player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        camFollow.SetFollower(Player.gameObject);

        // Hide main menu, show top UI, etc.
        GUIManager.Instance.ShowMainMenu(false);
        GUIManager.Instance.ShowTopUI(true);
        GUIManager.Instance.ShowWeaponPicker(weapons);

        ExpManager.SetUp();
    }

    public void GameOver()
    {
        GlobalNumerals.CanMove = false;
        GlobalNumerals.SpawnEnemies = false;

        // Show game over UI or perform any other end game logic here
        Debug.Log("Game Over");
    }
}
