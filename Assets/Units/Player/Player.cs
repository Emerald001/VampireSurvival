using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public float Speed { get; set; }
    public bool Dead { get; set; }

    private UnitVisuals visuals;
    private WeaponHolder weaponHolder;

    private bool canMove = true;

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
        if (Dead)
            return;

        Health -= damage;
        visuals.UpdateHealthBar(Health, MaxHealth);
        CameraShake.Shake(0.1f);

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
            Dead = true;

            weaponHolder.gameObject.SetActive(false);
            visuals.gameObject.SetActive(false);

            EnemyManager.Instance.DoKnockback(10f, transform.position);
            GameManager.Instance.GameOver();
        });
    }
}
