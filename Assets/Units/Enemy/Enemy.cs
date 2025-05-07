using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public float Speed { get; set; }

    private UnitVisuals enemyVisuals;
    private WeaponHolder weaponHolder;
    private bool canMove = true;

    public void SetData(EnemyConfig config)
    {
        Health = config.health;
        MaxHealth = config.health;
        Speed = config.speed;

        enemyVisuals = GetComponentInChildren<UnitVisuals>();
        weaponHolder = GetComponentInChildren<WeaponHolder>();

        weaponHolder.EquipWeapon(config.weaponConfig);
    }

    private void FixedUpdate()
    {
        var target = GameManager.Instance.Player.transform;
        if (Vector3.Distance(transform.position, target.position) > weaponHolder.EquippedWeapon?.Range)
            Move(target.position);
    }

    public void Move(Vector3 targetPosition)
    {
        if (!GlobalNumerals.CanMove || !canMove)
            return;

        var dir = (targetPosition - transform.position).normalized;
        var newPosition = transform.position + dir * Speed * Time.deltaTime;

        var bounds = GameManager.Instance.MapSize;
        float padding = 0.6f; // Padding value

        newPosition.x = Mathf.Clamp(newPosition.x, -(bounds.x / 2) + padding, (bounds.x / 2) - padding);
        newPosition.y = Mathf.Clamp(newPosition.y, -(bounds.y / 2) + padding, (bounds.y / 2) - padding);

        transform.position = newPosition;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        enemyVisuals.UpdateHealthBar(Health, MaxHealth);
        if (Health <= 0)
        {
            Die();
            return;
        }
        enemyVisuals.PlayHitAnimation();
    }

    public void TakeKnockback(Vector2 dir, float power)
    {
        canMove = false;
        
        float knockbackDuration = 0.5f;
        Vector3 knockbackTarget = transform.position + (Vector3)(dir.normalized * power);

        LeanTween.cancel(gameObject);
        LeanTween.move(gameObject, knockbackTarget, knockbackDuration)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() => canMove = true);
    }

    public void Die()
    {
        weaponHolder.Stop();

        enemyVisuals.PlayDeathAnimation(() =>
        {
            EnemyManager.spawnedEnemies.Remove(this);
            Destroy(gameObject);
        });
    }
}
