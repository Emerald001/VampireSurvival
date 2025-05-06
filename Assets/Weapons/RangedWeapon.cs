using UnityEngine;

public class RangedWeapon : Weapon
{
    public Projectile ProjectilePrefab;

    public override void Fire(Vector2 direction)
    {
        Debug.Log($"Firing ranged weapon for {Damage} damage!");
        if (ProjectilePrefab != null)
        {
            // Instantiate and shoot the projectile
            Projectile projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * Range;
        }
    }
}