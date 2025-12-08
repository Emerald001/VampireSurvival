using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/PlayerConfig", order = 3)]
public class PlayerConfig : UnitBaseStats
{
    [Header("Player Identity")]
    public string playerName;

    [Header("Starting Equipment")]
    public List<PlayerDecorationConfig> startingDecorations = new();
}
