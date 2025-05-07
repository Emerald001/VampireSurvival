using UnityEngine;

public class Weapon
{
    public virtual WeaponHolder Owner { get; set; }

    public virtual int Damage { get; set; }
    public virtual float Range { get; set; }
    public virtual float FireRate { get; set; }

    public void Initialize(WeaponHolder owner, WeaponConfig config)
    {
        Owner = owner;

        Damage = config.damage;
        Range = config.attackRange;
        FireRate = config.fireRate;
    }

    public virtual void Fire(Vector2 direction) { }
}
