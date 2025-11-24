using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public List<StatUpgrade> upgrades = new List<StatUpgrade>();
    private UnitBaseStats baseStats;
    private WeaponConfig weaponConfig;

    public float Health { get { if (statsDirty) RecalculateStats(); return cachedHealth; } }
    public float Speed { get { if (statsDirty) RecalculateStats(); return cachedSpeed; } }

    public float Damage { get { if (statsDirty) RecalculateStats(); return cachedDamage; } }
    public float AttackRange { get { if (statsDirty) RecalculateStats(); return cachedAttackRange; } }
    public float FireRate { get { if (statsDirty) RecalculateStats(); return cachedFireRate; } }

    private float cachedHealth;
    private float cachedSpeed;
    private float cachedDamage;
    private float cachedAttackRange;
    private float cachedFireRate;

    private bool statsDirty = true;

    public void SetBaseStats(UnitBaseStats baseStats)
    {
        this.baseStats = baseStats;
        statsDirty = true;
    }

    public void SetWeaponConfig(WeaponConfig weaponConfig)
    {
        this.weaponConfig = weaponConfig;
        statsDirty = true;
    }

    public void AddUpgrade(StatUpgrade upgrade)
    {
        upgrades.Add(upgrade);
        statsDirty = true;
    }

    public void RemoveUpgrade(StatUpgrade upgrade)
    {
        upgrades.Remove(upgrade);
        statsDirty = true;
    }

    private void RecalculateStats()
    {
        cachedHealth = GetUpgradedStat(baseStats.health, StatUpgrade.UpgradeType.Health);
        cachedSpeed = GetUpgradedStat(baseStats.speed, StatUpgrade.UpgradeType.Speed);
        cachedDamage = GetUpgradedStat(weaponConfig.damage, StatUpgrade.UpgradeType.Damage);
        cachedAttackRange = GetUpgradedStat(weaponConfig.attackRange, StatUpgrade.UpgradeType.AttackRange);
        cachedFireRate = GetUpgradedStat(weaponConfig.fireRate, StatUpgrade.UpgradeType.FireRate);
        statsDirty = false;
    }

    private float GetUpgradedStat(float baseValue, StatUpgrade.UpgradeType type)
    {
        float value = baseValue;
        foreach (var upgrade in upgrades)
        {
            if (upgrade.upgradeType == type)
                upgrade.Apply(ref value);
        }
        return value;
    }
}
