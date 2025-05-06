using UnityEngine;

public enum WeaponDecorationType
{
    DamageUpgrade,
    FireRateUpgrade
}

// Base Weapon Decorator
public abstract class WeaponDecorator : Weapon
{
    protected Weapon _baseWeapon;

    public WeaponDecorator(Weapon baseWeapon)
    {
        _baseWeapon = baseWeapon;
    }

    public override void Fire(Vector2 direction)
    {
        _baseWeapon.Fire(direction);
    }
}

public class DamageUpgrade : WeaponDecorator
{
    private int _extraDamage;

    public DamageUpgrade(Weapon baseWeapon, int extraDamage) : base(baseWeapon)
    {
        _extraDamage = extraDamage;
        Damage = _baseWeapon.Damage + _extraDamage;
    }

    public override void Fire(Vector2 direction)
    {
        Debug.Log($"Upgraded weapon with extra damage: {_extraDamage}");
        base.Fire(direction);
    }
}

public class FireRateUpgrade : WeaponDecorator
{
    private float _extraFireRate;

    public FireRateUpgrade(Weapon baseWeapon, float extraFireRate) : base(baseWeapon)
    {
        _extraFireRate = extraFireRate;
        FireRate = _baseWeapon.FireRate - _extraFireRate;
    }

    public override void Fire(Vector2 direction)
    {
        Debug.Log($"Upgraded weapon with faster fire rate: {_extraFireRate}");
        base.Fire(direction);
    }
}
