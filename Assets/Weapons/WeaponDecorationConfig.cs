using System;
using UnityEngine;

[Serializable]
public class WeaponDecorationConfig
{
    public WeaponDecorationType decorationType;

    // Add specific properties for each decoration type
    public Projectile projectilePrefab; // For RangedDecoration
    public MeleeSwingAnimation meleeSwingAnimation; // For MeleeDecoration
    public float weaponSize; // For MeleeDecoration

    public int extraDamage; // For DamageUpgrade
    public float extraFireRate; // For FireRateUpgrade
    public float areaOfEffectRadius; // For AreaOfEffectUpgrade
    public float projectileSpeed; // For ProjectileSpeedUpgrade
    public float knockbackForce; // For KnockbackUpgrade

    // Add more fields as needed for other decoration types
}

[Serializable]
public class MeleeSwingAnimation
{
    public AnimationCurve distanceCurve; // Curve for distance from the player
    public float swingAngle; // Total angle covered by the swing (e.g., 90 degrees)
    public float duration; // Duration of the swing in seconds
}
