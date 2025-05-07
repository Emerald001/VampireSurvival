using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickOption : MonoBehaviour
{
    [SerializeField] private Button button;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI fireRate;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI range;

    private WeaponConfig weapon;

    public void SetData(WeaponConfig weapon, System.Action onClick)
    {
        this.weapon = weapon;
        button.onClick.AddListener(OnClick);
        button.onClick.AddListener(() => onClick.Invoke());

        icon.sprite = weapon.icon;
        weaponName.text = weapon.weaponName;

        fireRate.text = weapon.fireRate.ToString();
        damage.text = weapon.damage.ToString();
        range.text = weapon.attackRange.ToString();
    }

    public void OnClick()
    {
        GameManager.Instance.Player.EquipWeapon(weapon);
        GUIManager.Instance.ToggleWeaponPicker(false);
    }
}