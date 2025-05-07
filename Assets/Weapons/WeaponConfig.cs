using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/WeaponConfig", order = 2)]
public class WeaponConfig : ScriptableObject
{
    public string weaponName;
    public string description;
    public Sprite icon;

    public int damage;
    public float fireRate;
    public float attackRange;

    // Change this to hold WeaponDecorationConfig references
    public List<WeaponDecorationConfig> weaponDecorations = new();

    public GameObject weaponPrefab;
}

public enum WeaponDecorationType
{
    MeleeDecoration,
    RangedDecoration,
    DamageUpgrade,
    FireRateUpgrade,
    Multishot,
    AreaOfEffectUpgrade,
    CriticalHitUpgrade,
    ElementalUpgrade,
    KnockbackUpgrade,
    LifestealUpgrade,
    RangeUpgrade,
}

public enum ProjectileDecorationtype
{
    Bounce,
    Pierce,
    AreaOfEffect,
    CriticalHit,
    Elemental,
    Knockback,
    Lifesteal,
    Speed,
}
