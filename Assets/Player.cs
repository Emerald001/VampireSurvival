using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public float Speed { get; set; }

    private UnitVisuals visuals;

    private void Start()
    {
        visuals = GetComponentInChildren<UnitVisuals>();

        Health = 100;
        Speed = 5f;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (!GlobalNumerals.CanMove)
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

    public void TakeDamage(int damage)
    {
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
        visuals.PlayDeathAnimation();
    }
}

public class PlayerModifiers
{

}
