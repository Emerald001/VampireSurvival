using TMPro;
using UnityEngine;

public class WeaponPickOption : PickOption<WeaponConfig>
{
    [SerializeField] private TextMeshProUGUI fireRate;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI range;

    public override void SetData(WeaponConfig weapon, int index)
    {
        base.SetData(weapon, index);
        
        nameText.text = weapon.weaponName;
        icon.sprite = weapon.icon;

        fireRate.text = weapon.fireRate.ToString();
        damage.text = weapon.damage.ToString();
        range.text = weapon.attackRange.ToString();
    }
}
