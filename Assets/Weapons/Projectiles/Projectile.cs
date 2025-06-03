using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage = 10;

    public void SetData(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageable = other.GetComponent<IDamageable>();

        // Check if the projectile hits an enemy
        if (damageable != null)
            OnHit(damageable);
    }

    private void OnHit(IDamageable damageable)
    {
        damageable.TakeDamage(damage);

        Destroy(gameObject);
    }
}
