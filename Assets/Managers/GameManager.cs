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

    [SerializeField] private Transform map;
    [SerializeField] private AreaConfig areaConfig;
    [SerializeField] private UnitBaseStats playerStats;

    public Player Player { get; private set; }
    public AreaConfig CurrentArea => currentArea;
    private AreaConfig currentArea;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        SetArea(areaConfig);
    }

    private void OnDisable()
    {
        GlobalNumerals.SpawnEnemies = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.V))
            GUIManager.Instance.ShowNewGameStartScreen(false);
    }

    public void SetArea(AreaConfig area)
    {
        currentArea = area;
        var mapSize = area.mapSize;

        map.localScale = new Vector3(mapSize.x / 10, 1, mapSize.y / 10);
        map.GetComponent<Renderer>().material.mainTextureScale = new Vector2(mapSize.x, mapSize.y);
    }

    public void StartGame()
    {
        Player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        Player.SetData(playerStats);
        camFollow.SetFollower(Player.gameObject);

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
