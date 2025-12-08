using System;
using UnityEngine;

[Serializable]
public class PlayerDecorationConfig
{
    [Header("Basic Info")]
    public string decorationName;
    public Sprite icon;
    [TextArea(2, 4)]
    public string description;

    [Header("Decoration Type")]
    public PlayerDecorationType decorationType;

    [Header("Upgrade Values")]
    public float healthBonus;
    public float speedBonus;
    public float damageMultiplier = 1f;
    public float fireRateMultiplier = 1f;
    public float experienceMultiplier = 1f;
    public float pickupRangeBonus;
    public float damageResistance;
    public float healthRegeneration;
}

public enum PlayerDecorationType
{
    HealthUpgrade,
    SpeedUpgrade,
    DamageUpgrade,
    FireRateUpgrade,
    ExperienceBoost,
    PickupRangeUpgrade,
    DamageResistance,
    HealthRegeneration,
    MovementSpeed,
    MaxHealthUpgrade
}
