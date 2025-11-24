using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable
{
    public UnitStats Stats { get; protected set; }
    public float Health { get; set; }
    public bool Dead { get; set; }

    protected UnitVisuals unitVisuals;
    protected WeaponHolder weaponHolder;
    protected bool canMove = true;

    public virtual void SetData(UnitBaseStats config)
    {
        Stats = gameObject.AddComponent<UnitStats>();
        Stats.SetBaseStats(config);

        unitVisuals = GetComponentInChildren<UnitVisuals>();
        weaponHolder = GetComponentInChildren<WeaponHolder>();
    }

    public abstract void Move(Vector3 targetPosition);
    public abstract void TakeDamage(float damage);
    public abstract void TakeKnockback(Vector2 dir, float power);
    public abstract void Die();
}
