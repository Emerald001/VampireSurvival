using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private bool playerOwned;

    public Weapon EquippedWeapon { get; private set; }

    private List<WeaponDecorator> decorators = new();
    private GameObject weaponObject;
    private float fireCooldown = 0f;

    public void EquipWeapon(WeaponConfig weapon)
    {
        if (EquippedWeapon != null && weaponObject != null)
            Destroy(weaponObject);

        weaponObject = Instantiate(weapon.weaponPrefab, transform.position, Quaternion.identity);
        weaponObject.transform.SetParent(transform);

        EquippedWeapon = new();
        EquippedWeapon.Initialize(this, weapon);

        // Apply all decorations from the WeaponConfig
        foreach (var decorationConfig in weapon.weaponDecorations)
            ApplyUpgrade(decorationConfig);
    }

    public void Stop()
    {
        EquippedWeapon = null;
    }

    public void ApplyUpgrade(WeaponDecorationConfig config)
    {
        if (config == null)
            return;

        WeaponDecorator decorator = config.decorationType switch
        {
            WeaponDecorationType.RangedDecoration =>
                new RangedWeapon(EquippedWeapon, config.projectilePrefab),
            WeaponDecorationType.MeleeDecoration =>
                new MeleeWeapon(EquippedWeapon, config.meleeSwingAnimation, weaponObject.GetComponent<MeleeHitbox>(), config.weaponSize),
            WeaponDecorationType.DamageUpgrade =>
                new DamageUpgrade(EquippedWeapon, config.extraDamage),
            WeaponDecorationType.FireRateUpgrade =>
                new FireRateUpgrade(EquippedWeapon, config.extraFireRate),
            _ => null
        };

        if (decorator == null)
            throw new NotSupportedException($"Unsupported decoration type: {config.decorationType}");

        EquippedWeapon = decorator;
        decorators.Add(decorator);
    }

    private void Update()
    {
        if (EquippedWeapon == null)
            return;

        Transform closestEnemy = GetClosestEnemyInRange();
        if (closestEnemy == null)
            return;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, closestEnemy.position - transform.position);
        transform.rotation = rotation;

        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Vector2 direction = (closestEnemy.position - transform.position).normalized;
            EquippedWeapon.Fire(direction);
            fireCooldown = 1f / EquippedWeapon.FireRate; // Reset cooldown based on fire rate
        }
    }

    private Transform GetClosestEnemyInRange()
    {
        Vector3 playerPosition = GameManager.Instance.Player.transform.position;

        if (playerOwned)
        {
            return EnemyManager.spawnedEnemies
                .Where(enemy => Vector3.Distance(playerPosition, enemy.transform.position) <= EquippedWeapon.Range)
                .OrderBy(enemy => Vector3.Distance(playerPosition, enemy.transform.position))
                .FirstOrDefault()?.transform;
        }
        else
        {
            if (Vector3.Distance(playerPosition, transform.position) <= EquippedWeapon.Range)
                return GameManager.Instance.Player.transform;
        }
        return null;
    }
}
