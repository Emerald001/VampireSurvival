using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Weapon equippedWeapon;
    [SerializeField] private Transform weaponHolderTransform;

    private GameObject weaponObject;
    private float fireCooldown = 0f;

    private List<WeaponDecorator> decorators = new();

    private void Start()
    {
        weaponHolderTransform = transform;
    }

    public void EquipWeapon(WeaponConfig weapon)
    {
        if (equippedWeapon != null && weaponObject != null)
            Destroy(weaponObject);

        weaponObject = Instantiate(weapon.weaponPrefab, weaponHolderTransform.position, Quaternion.identity);
        weaponObject.transform.SetParent(weaponHolderTransform);

        equippedWeapon = new();
        equippedWeapon.Initialize(this, weapon);

        // Apply all decorations from the WeaponConfig
        foreach (var decorationConfig in weapon.weaponDecorations)
            ApplyUpgrade(decorationConfig);
    }

    public void ApplyUpgrade(WeaponDecorationConfig config)
    {
        if (config == null)
            return;

        WeaponDecorator decorator = config.decorationType switch
        {
            WeaponDecorationType.RangedDecoration =>
                new RangedWeapon(equippedWeapon, config.projectilePrefab),
            WeaponDecorationType.DamageUpgrade =>
                new DamageUpgrade(equippedWeapon, config.extraDamage),
            WeaponDecorationType.FireRateUpgrade =>
                new FireRateUpgrade(equippedWeapon, config.extraFireRate),
            _ => null
        };

        if (decorator == null)
            throw new NotSupportedException($"Unsupported decoration type: {config.decorationType}");

        equippedWeapon = decorator;
        decorators.Add(decorator);
    }

    private void Update()
    {
        if (equippedWeapon == null || EnemyManager.spawnedEnemies.Count == 0)
            return;

        Enemy closestEnemy = GetClosestEnemyInRange();
        if (closestEnemy == null)
            return;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, closestEnemy.transform.position - weaponHolderTransform.position);
        weaponHolderTransform.rotation = rotation;

        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Vector2 direction = (closestEnemy.transform.position - weaponHolderTransform.position).normalized;
            equippedWeapon.Fire(direction);
            fireCooldown = 1f / equippedWeapon.FireRate; // Reset cooldown based on fire rate
        }
    }

    private Enemy GetClosestEnemyInRange()
    {
        Vector3 playerPosition = GameManager.Instance.Player.transform.position;

        return EnemyManager.spawnedEnemies
            .Where(enemy => Vector3.Distance(playerPosition, enemy.transform.position) <= equippedWeapon.Range)
            .OrderBy(enemy => Vector3.Distance(playerPosition, enemy.transform.position))
            .FirstOrDefault();
    }
}
