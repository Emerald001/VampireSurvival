using TMPro;
using UnityEngine;

public class WeaponDecorationPickOption : PickOption<WeaponDecorationConfig>
{
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI statsText;

    private WeaponDecorationConfig decoration;

    public override void SetData(WeaponDecorationConfig decoration, int index)
    {
        base.SetData(decoration, Index);
        
        nameText.text = GetDecorationDisplayName(decoration.decorationType);

        // Note: WeaponDecorationConfig doesn't have an icon field,
        // you may want to add one or use a default icon based on type
        // icon.sprite = decoration.icon;

        if (typeText != null)
            typeText.text = decoration.decorationType.ToString();

        if (statsText != null)
            statsText.text = GetStatsDisplay(decoration);

        if (descriptionText != null)
            descriptionText.text = GetDescription(decoration);
    }

    private string GetDecorationDisplayName(WeaponDecorationType type)
    {
        return type switch
        {
            WeaponDecorationType.MeleeDecoration => "Melee Attack",
            WeaponDecorationType.RangedDecoration => "Ranged Attack",
            WeaponDecorationType.DamageUpgrade => "Damage Boost",
            WeaponDecorationType.FireRateUpgrade => "Fire Rate Boost",
            WeaponDecorationType.Multishot => "Multishot",
            WeaponDecorationType.AreaOfEffectUpgrade => "Area of Effect",
            WeaponDecorationType.CriticalHitUpgrade => "Critical Hit",
            WeaponDecorationType.ElementalUpgrade => "Elemental Damage",
            WeaponDecorationType.KnockbackUpgrade => "Knockback",
            WeaponDecorationType.LifestealUpgrade => "Lifesteal",
            WeaponDecorationType.RangeUpgrade => "Range Boost",
            _ => type.ToString()
        };
    }

    private string GetStatsDisplay(WeaponDecorationConfig decoration)
    {
        return decoration.decorationType switch
        {
            WeaponDecorationType.DamageUpgrade => $"+{decoration.extraDamage} Damage",
            WeaponDecorationType.FireRateUpgrade => $"+{decoration.extraFireRate:F1} Fire Rate",
            WeaponDecorationType.AreaOfEffectUpgrade => $"{decoration.areaOfEffectRadius}m Radius",
            WeaponDecorationType.KnockbackUpgrade => $"{decoration.knockbackForce} Force",
            _ => ""
        };
    }

    private string GetDescription(WeaponDecorationConfig decoration)
    {
        return decoration.decorationType switch
        {
            WeaponDecorationType.MeleeDecoration => "Adds melee swing attack",
            WeaponDecorationType.RangedDecoration => "Fires projectiles",
            WeaponDecorationType.DamageUpgrade => "Increases weapon damage",
            WeaponDecorationType.FireRateUpgrade => "Increases attack speed",
            WeaponDecorationType.Multishot => "Fires multiple projectiles",
            WeaponDecorationType.AreaOfEffectUpgrade => "Damage in an area",
            WeaponDecorationType.CriticalHitUpgrade => "Chance for critical hits",
            WeaponDecorationType.ElementalUpgrade => "Adds elemental effects",
            WeaponDecorationType.KnockbackUpgrade => "Pushes enemies back",
            WeaponDecorationType.LifestealUpgrade => "Heals on hit",
            WeaponDecorationType.RangeUpgrade => "Increases attack range",
            _ => ""
        };
    }
}
