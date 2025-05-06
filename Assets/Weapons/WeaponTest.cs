using UnityEngine;

public class WeaponTest : MonoBehaviour
{
    private Weapon meleeWeapon;
    private Weapon rangedWeapon;

    private void Start()
    {
        // Create a ranged weapon
        rangedWeapon = new RangedWeapon();
        rangedWeapon.Damage = 15;
        rangedWeapon.Range = 20f;

        // Upgrade the ranged weapon
        rangedWeapon = new FireRateUpgrade(rangedWeapon, 0.2f);
        rangedWeapon.Fire(Vector2.right);
    }
}
