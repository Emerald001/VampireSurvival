using TMPro;
using UnityEngine;

public class PlayerDecorationPickOption : PickOption<PlayerDecorationConfig>
{
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI statsText;

    private PlayerDecorationConfig decoration;

    public override void SetData(PlayerDecorationConfig decoration, int index)
    {
        base.SetData(decoration, Index);
        
        nameText.text = decoration.decorationName;
        icon.sprite = decoration.icon;

        if (typeText != null)
            typeText.text = decoration.decorationType.ToString();

        if (statsText != null)
            statsText.text = GetStatsDisplay(decoration);

        if (descriptionText != null)
            descriptionText.text = decoration.description;
    }

    private string GetStatsDisplay(PlayerDecorationConfig decoration)
    {
        return decoration.decorationType switch
        {
            PlayerDecorationType.HealthUpgrade => $"+{decoration.healthBonus} Health",
            PlayerDecorationType.SpeedUpgrade => $"+{decoration.speedBonus} Speed",
            PlayerDecorationType.DamageUpgrade => $"x{decoration.damageMultiplier:F2} Damage",
            PlayerDecorationType.FireRateUpgrade => $"x{decoration.fireRateMultiplier:F2} Fire Rate",
            PlayerDecorationType.ExperienceBoost => $"x{decoration.experienceMultiplier:F2} XP",
            PlayerDecorationType.PickupRangeUpgrade => $"+{decoration.pickupRangeBonus}m Pickup Range",
            PlayerDecorationType.DamageResistance => $"{decoration.damageResistance * 100}% Resistance",
            PlayerDecorationType.HealthRegeneration => $"+{decoration.healthRegeneration}/s Regen",
            PlayerDecorationType.MaxHealthUpgrade => $"+{decoration.healthBonus} Max Health",
            _ => ""
        };
    }
}
