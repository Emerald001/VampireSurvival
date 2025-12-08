using TMPro;
using UnityEngine;

public class PlayerPickOption : PickOption<UnitBaseStats>
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI speedText;

    public override void SetData(UnitBaseStats playerConfig, int index)
    {
        base.SetData(playerConfig, index);
        
        nameText.text = playerConfig.unitName;
        icon.sprite = playerConfig.unitIcon;

        healthText.text = $"Health: {playerConfig.health}";
        speedText.text = $"Speed: {playerConfig.speed}";
    }
}
