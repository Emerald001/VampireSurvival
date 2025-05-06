using UnityEngine;
using System.Threading.Tasks;

public class Enemy : MonoBehaviour, IDamageable
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public float Speed { get; set; }

    private UnitVisuals enemyVisuals;

    public void SetData(EnemyConfig config)
    {
        Health = config.health;
        MaxHealth = config.health;
        Speed = config.speed;

        enemyVisuals = GetComponent<UnitVisuals>();
        //enemyVisuals.body.sprite = config.enemyModel;
        //enemyVisuals.deathEffect = config.deathEffect;
    }

    private async void Update()
    {
        var target = GameManager.Instance.Player.transform;
        //if (Vector3.Distance(transform.position, target.position) < AttackRange)
        //    await Attack();
        //else
            Move(target.position);
    }

    public virtual async Task Attack()
    {
        var target = GameManager.Instance.Player.transform;
        //if (Vector3.Distance(transform.position, target.position) < AttackRange)
        //    enemyVisuals.PlayAttackAnimation();

        // Do damage via weapon
    }

    public void Move(Vector3 targetPosition)
    {
        if (!GlobalNumerals.CanMove)
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

    public void Die()
    {
        Debug.Log("Enemy has died.");
        enemyVisuals.PlayDeathAnimation();
        Destroy(gameObject);
    }
}
