using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitBaseStats", menuName = "ScriptableObjects/UnitBaseStats", order = 1)]
public class UnitBaseStats : ScriptableObject
{
    public float health = 100f;
    public float speed = 5f;
    public float damage = 10f;
    public float attackRange = 1.5f;
    public float fireRate = 1f;
    public float attackSpeed = 1f;
}

public class UnitStats
{
    public List<StatUpgrade> upgrades;

    public float health;
    public float speed;
    public float damage;
    public float attackRange;
    public float fireRate;
    public float attackSpeed;

    public UnitStats(UnitBaseStats baseStats)
    {
        health = baseStats.health;
        speed = baseStats.speed;
        damage = baseStats.damage;
        attackRange = baseStats.attackRange;
        fireRate = baseStats.fireRate;
        attackSpeed = baseStats.attackSpeed;
    }

    public void ApplyUpgrade(StatUpgrade upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case StatUpgrade.UpgradeType.Health:
                upgrade.Apply(ref health);
                break;
            case StatUpgrade.UpgradeType.Speed:
                upgrade.Apply(ref speed);
                break;
            case StatUpgrade.UpgradeType.Damage:
                upgrade.Apply(ref damage);
                break;
            case StatUpgrade.UpgradeType.AttackRange:
                upgrade.Apply(ref attackRange);
                break;
            case StatUpgrade.UpgradeType.FireRate:
                upgrade.Apply(ref fireRate);
                break;
            case StatUpgrade.UpgradeType.AttackSpeed:
                upgrade.Apply(ref attackSpeed);
                break;
        }
    }
}
