using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/WeaponConfig", order = 2)]
public class WeaponConfig : ScriptableObject
{
    public int damage;
    public float attackSpeed;
    public float attackRange;
    public GameObject weaponPrefab;
}
