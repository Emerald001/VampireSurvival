using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public float Speed { get; set; }

    private UnitVisuals visuals;
    private WeaponHolder weaponHolder;

    private bool canMove = true;
    private bool dead = false;

    private void Start()
    {
        visuals = GetComponentInChildren<UnitVisuals>();
        weaponHolder = GetComponentInChildren<WeaponHolder>();

        MaxHealth = 10;
        Health = MaxHealth;
        Speed = 5f;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (!GlobalNumerals.CanMove || !canMove)
            return;

        var dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        var newPosition = transform.position + dir * Speed * Time.deltaTime;

        // Clamp the position to stay within the defined boundaries with padding
        var bounds = GameManager.Instance.MapSize;
        float padding = 0.6f; // Padding value

        newPosition.x = Mathf.Clamp(newPosition.x, -(bounds.x / 2) + padding, (bounds.x / 2) - padding);
        newPosition.y = Mathf.Clamp(newPosition.y, -(bounds.y / 2) + padding, (bounds.y / 2) - padding);

        transform.position = newPosition;
    }

    public void EquipWeapon(WeaponConfig weapon)
    {
        weaponHolder.EquipWeapon(weapon);
    }

    public void TakeDamage(int damage)
    {
        if (dead)
            return;

        Health -= damage;
        visuals.UpdateHealthBar(Health, MaxHealth);

        if (Health <= 0)
        {
            Die();
            return;
        }

        visuals.PlayHitAnimation();
    }

    public void Die()
    {
        Debug.Log("Player has died.");
        visuals.PlayDeathAnimation(() => 
        {
            weaponHolder.Stop();
            canMove = false;
            dead = true;

            foreach (var item in EnemyManager.spawnedEnemies)
            {
                var direction = (item.transform.position - transform.position).normalized;
                var dis = Vector3.Distance(item.transform.position, transform.position);

                if (dis < 10)
                    item.TakeKnockback(direction, (10 - dis) / 2);
            }

            // Optionally, you can disable the player object or trigger a game over screen here
            GameManager.Instance.GameOver();
        });
    }
}

public class PlayerModifiers
{

}
