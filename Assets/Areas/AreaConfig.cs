using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Area", menuName = "ScriptableObjects/AreaConfig")]
public class AreaConfig : ScriptableObject
{
    public string areaName;
    public Vector2Int mapSize;

    public List<WaveConfig> waves = new();
}

[System.Serializable]
public class WaveConfig
{
    public float delayBeforeStart;
    public List<WavePart> waveParts = new();
}

[System.Serializable]
public class WavePart
{
    public int amountToSpawn;
    public float delayBetweenSpawns;
    public List<EnemyConfig> enemyConfigs = new();
}
