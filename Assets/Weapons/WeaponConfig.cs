using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/WeaponConfig", order = 2)]
public class WeaponConfig : ScriptableObject
{
    public string weaponName;
    public string description;
    public Sprite icon;

    public int damage;
    public float attackSpeed;
    public float attackRange;

    public WeaponType weaponType;

    public GameObject weaponPrefab;
}

public enum WeaponType
{
    Melee,
    Ranged
}
