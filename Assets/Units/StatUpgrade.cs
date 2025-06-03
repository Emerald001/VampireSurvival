using UnityEngine;

[CreateAssetMenu(fileName = "StatUpgrade", menuName = "ScriptableObjects/StatUpgrade", order = 2)]
public class StatUpgrade : ScriptableObject
{
    public enum UpgradeType
    {
        Health,
        Speed,
        Damage,
        AttackRange,
        FireRate,
        AttackSpeed,
    }

    public float multiplier = 1f;
    public UpgradeType upgradeType;

    public StatUpgrade(float multiplier)
    {
        this.multiplier = multiplier;
    }

    public StatUpgrade(StatUpgrade upgrade)
    {
        multiplier = upgrade.multiplier;
    }

    public void Apply(ref float value)
    {
        value *= multiplier;
    }
}
