using System.Collections.Generic;
using UnityEngine;

// Base Weapon Decorator
public abstract class WeaponDecorator : Weapon
{
    public override WeaponHolder Owner => _baseWeapon.Owner;

    public override float Damage => _baseWeapon.Damage;
    public override float Range => _baseWeapon.Range;
    public override float FireRate => _baseWeapon.FireRate;

    protected Weapon _baseWeapon;

    public WeaponDecorator(Weapon baseWeapon)
    {
        _baseWeapon = baseWeapon;
    }

    public override void Fire(Vector2 direction)
    {
        _baseWeapon.Fire(direction);
    }
}

public class MeleeWeapon : WeaponDecorator
{
    private readonly MeleeSwingAnimation _swingAnimation;
    private readonly MeleeHitbox _weaponTransform; // Reference to the weapon's transform
    private readonly Collider2D weaponCollider;

    public MeleeWeapon(Weapon baseWeapon, MeleeSwingAnimation swingAnimation, MeleeHitbox weaponTransform, float weaponSize) : base(baseWeapon)
    {
        _swingAnimation = swingAnimation;
        _weaponTransform = weaponTransform;

        _weaponTransform.SetSize(weaponSize); // Set the size of the melee hitbox

        weaponCollider = weaponTransform.GetComponentInChildren<Collider2D>();
        weaponCollider.enabled = false; // Disable the collider initially
    }

    public override void Fire(Vector2 direction)
    {
        _weaponTransform.Damage = Damage; // Set the damage for the melee hitbox
        Owner.StartCoroutine(Swing());
    }

    private System.Collections.IEnumerator Swing()
    {
        float elapsedTime = 0f;
        float startAngle = -_swingAnimation.swingAngle / 2f;
        float endAngle = _swingAnimation.swingAngle / 2f;

        weaponCollider.enabled = true;
        while (elapsedTime < _swingAnimation.duration)
        {
            float t = elapsedTime / _swingAnimation.duration;
            float currentAngle = Mathf.Lerp(startAngle, endAngle, t);
            float distance = _swingAnimation.distanceCurve.Evaluate(t);

            _weaponTransform.transform.localPosition = new Vector3(0, distance, 0);
            _weaponTransform.transform.localRotation = Quaternion.Euler(0, 0, currentAngle);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        weaponCollider.enabled = false;

        _weaponTransform.transform.localPosition = Vector3.zero;
        _weaponTransform.transform.localRotation = Quaternion.identity;
    }
}

public class RangedWeapon : WeaponDecorator
{
    private Projectile projectilePrefab;
    private float projectileSpeed = 10f;

    private List<ProjectileDecorationtype> _projectileDecorations = new List<ProjectileDecorationtype>();

    public RangedWeapon(Weapon baseWeapon, Projectile projectilePrefab) : base(baseWeapon)
    {
        this.projectilePrefab = projectilePrefab;
    }

    public override void Fire(Vector2 direction)
    {
        // Instantiate and shoot the projectile
        Projectile projectile = Object.Instantiate(projectilePrefab, Owner.transform.position, Quaternion.identity);
        projectile.transform.position = Owner.transform.position + (Vector3)direction;
        projectile.SetData(Damage);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed;

        // Rotate the projectile to face the direction it is moving
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        projectile.transform.rotation = rotation;
    }
}
