using System;
using UnityEngine;

[Serializable]
public class ProjectileDecorationConfig
{
    [Header("Basic Info")]
    public string decorationName;
    public Sprite icon;
    [TextArea(2, 4)]
    public string description;

    [Header("Decoration Type")]
    public ProjectileDecorationType decorationType;

    [Header("Bounce Settings")]
    public int bounceCount;
    public float bounceAngleRandomness = 15f;

    [Header("Pierce Settings")]
    public int pierceCount;
    public float pierceDamageMultiplier = 1f;

    [Header("Area of Effect")]
    public float areaOfEffectRadius;
    public float aoeDamageMultiplier = 0.8f;
    public GameObject aoeEffectPrefab;

    [Header("Critical Hit")]
    public float criticalChance = 0.1f;
    public float criticalDamageMultiplier = 2f;

    [Header("Elemental")]
    public ElementalType elementalType;
    public float elementalDamage;
    public GameObject elementalEffectPrefab;

    [Header("Knockback")]
    public float knockbackForce;
    public float knockbackDuration = 0.2f;

    [Header("Lifesteal")]
    public float lifestealPercentage = 0.1f;

    [Header("Speed Modification")]
    public float speedMultiplier = 1f;
    public float speedBonus;

    [Header("Homing")]
    public bool enableHoming;
    public float homingStrength = 5f;
    public float homingRadius = 10f;
}

public enum ProjectileDecorationType
{
    Bounce,
    Pierce,
    AreaOfEffect,
    CriticalHit,
    Elemental,
    Knockback,
    Lifesteal,
    Speed,
    Homing,
    SizeIncrease,
    DamageBoost
}

public enum ElementalType
{
    None,
    Fire,
    Ice,
    Lightning,
    Poison,
    Holy,
    Dark
}
