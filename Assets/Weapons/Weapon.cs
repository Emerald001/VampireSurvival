using UnityEngine;

public class Weapon
{
    public virtual WeaponHolder Owner { get; set; }

    public virtual float Damage => statInstance.Damage;
    public virtual float Range => statInstance.AttackRange;
    public virtual float FireRate => statInstance.FireRate;

    private UnitStats statInstance;

    public void Initialize(WeaponHolder owner, WeaponConfig config, UnitStats statInstance)
    {
        Owner = owner;
        this.statInstance = statInstance;
    }

    public virtual void Fire(Vector2 direction) { }
}
