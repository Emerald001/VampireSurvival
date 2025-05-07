using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private RectTransform weaponPickScreen;

    public void ToggleWeaponPicker(bool toggle)
    {
        weaponPickScreen.gameObject.SetActive(toggle);
    }
}
