using TMPro;
using UnityEngine;

public class ProjectilePickOption : PickOption<ProjectileConfig>
{
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private ProjectileConfig projectile;

    public override void SetData(ProjectileConfig projectile, int index)
    {
        base.SetData(projectile, index);
        
        nameText.text = projectile.projectileName;
        icon.sprite = projectile.icon;

        if (damageText != null)
            damageText.text = $"Damage: {projectile.baseDamage}";

        if (speedText != null)
            speedText.text = $"Speed: {projectile.speed}";

        if (descriptionText != null)
            descriptionText.text = projectile.description;
    }
}
