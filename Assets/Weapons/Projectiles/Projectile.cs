using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
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
