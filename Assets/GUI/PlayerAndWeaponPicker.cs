using UnityEngine;

public class PlayerAndWeaponPicker : MonoBehaviour
{
    [SerializeField] private OptionPicker weaponPicker;
    [SerializeField] private OptionPicker playerPicker;

    public RectTransform RectTransform => transform as RectTransform;

    public void SetPickers()
    {
        var gameManager = GameManager.Instance;
        var guiManager = GUIManager.Instance;

        weaponPicker.ClearOptions();
        playerPicker.ClearOptions();

        weaponPicker.SetOptions<WeaponConfig>(gameManager.Weapons, (X) => { }, true);
        playerPicker.SetOptions<UnitBaseStats>(gameManager.Players, (X) => { }, true);
    }

    public void Btn_StartGame()
    {
        var gameManager = GameManager.Instance;

        gameManager.CurrentPlayerConfig = playerPicker.GetSelectedItem<UnitBaseStats>();
        gameManager.StartGame();

        GameManager.Instance.Player.EquipWeapon(weaponPicker.GetSelectedItem<WeaponConfig>());
    }
}
