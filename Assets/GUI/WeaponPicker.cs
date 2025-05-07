using System.Collections.Generic;
using UnityEngine;

public class WeaponPicker : MonoBehaviour
{
    [SerializeField] private WeaponPickOption prefab;
    [SerializeField] private Transform content;

    [SerializeField] private List<WeaponConfig> weapons;

    private void Start()
    {
        foreach (var weapon in weapons)
        {
            var option = Instantiate(prefab, content);
            option.SetData(weapon, OnClick);
        }
    }

    private void OnClick()
    {
        GUIManager.Instance.ToggleWeaponPicker(false);
    }
}
