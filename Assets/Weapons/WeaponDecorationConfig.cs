using System;

[Serializable]
public class WeaponDecorationConfig
{
    public WeaponDecorationType decorationType;

    // Add specific properties for each decoration type
    public Projectile projectilePrefab; // For RangedDecoration

    public int extraDamage; // For DamageUpgrade
    public float extraFireRate; // For FireRateUpgrade
    public float areaOfEffectRadius; // For AreaOfEffectUpgrade
    public float projectileSpeed; // For ProjectileSpeedUpgrade
    public float knockbackForce; // For KnockbackUpgrade

    // Add more fields as needed for other decoration types
}
