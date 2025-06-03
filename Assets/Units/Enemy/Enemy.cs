using UnityEngine;

public class Enemy : Unit
{
    private EnemyConfig config;

    // Corrected method signature to match the base class
    public override void SetData(UnitBaseStats config)
    {
        base.SetData(config);

        this.config = config as EnemyConfig; // Cast to EnemyConfig
        if (this.config != null)
        {
            Health = this.config.health;
            weaponHolder.EquipWeapon(this.config.weaponConfig);
        }
    }

    private void FixedUpdate()
    {
        var target = GameManager.Instance.Player.transform;
        if (Vector3.Distance(transform.position, target.position) > weaponHolder.EquippedWeapon?.Range)
            Move(target.position);
    }

    public override void Move(Vector3 targetPosition)
    {
        if (!GlobalNumerals.CanMove || !canMove || Dead)
            return;

        var dir = (targetPosition - transform.position).normalized;
        var newPosition = transform.position + dir * Stats.speed * Time.deltaTime;

        var bounds = GameManager.Instance.CurrentArea.mapSize;
        float padding = 0.6f; // Padding value

        newPosition.x = Mathf.Clamp(newPosition.x, -(bounds.x / 2) + padding, (bounds.x / 2) - padding);
        newPosition.y = Mathf.Clamp(newPosition.y, -(bounds.y / 2) + padding, (bounds.y / 2) - padding);

        transform.position = newPosition;
    }

    public override void TakeDamage(int damage)
    {
        if (Dead)
            return;

        Health -= damage;
        unitVisuals.UpdateHealthBar(Health, Stats.health);

        if (Health <= 0)
        {
            Die();
            return;
        }
        unitVisuals.PlayHitAnimation();
    }

    public override void TakeKnockback(Vector2 dir, float power)
    {
        canMove = false;

        float knockbackDuration = 0.5f;
        Vector3 knockbackTarget = transform.position + (Vector3)(dir.normalized * power);

        LeanTween.cancel(gameObject);
        LeanTween.move(gameObject, knockbackTarget, knockbackDuration)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() => canMove = true);
    }

    public override void Die()
    {
        Dead = true;
        weaponHolder.Stop();

        if (config != null && config.Exp > 0)
        {
            for (int i = 0; i < config.Exp; i++)
            {
                Vector3 spawnPos = transform.position + Random.insideUnitSphere * 0.3f;
                spawnPos.z = 0;
                ExpierencePickup expItem = Instantiate(ResourceManager.ExpierencePickup, spawnPos, Quaternion.identity);
                expItem.SetData(config.Exp);
            }
        }

        unitVisuals.PlayDeathAnimation(() =>
        {
            EnemyManager.spawnedEnemies.Remove(this);
            Destroy(gameObject);
        });
    }
}

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
        Stats = new UnitStats(config);
        unitVisuals = GetComponentInChildren<UnitVisuals>();
        weaponHolder = GetComponentInChildren<WeaponHolder>();
    }

    public abstract void Move(Vector3 targetPosition);
    public abstract void TakeDamage(int damage);
    public abstract void TakeKnockback(Vector2 dir, float power);
    public abstract void Die();
}
