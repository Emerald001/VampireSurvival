using TMPro;
using UnityEngine;

public class WeaponPickOption : PickOption<WeaponConfig>
{
    [SerializeField] private TextMeshProUGUI fireRate;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI range;

    private WeaponConfig weapon;

    public override void SetData(WeaponConfig weapon)
    {
        this.weapon = weapon;

        button.onClick.AddListener(OnClick);
        nameText.text = weapon.weaponName;
        icon.sprite = weapon.icon;

        fireRate.text = weapon.fireRate.ToString();
        damage.text = weapon.damage.ToString();
        range.text = weapon.attackRange.ToString();
    }

    protected override void OnClick()
    {
        GameManager.Instance.Player.EquipWeapon(weapon);
        GUIManager.Instance.HideOptionPicker();

        GlobalNumerals.SpawnEnemies = true;
        EnemyManager.Instance.StartSpawning();
    }
}
