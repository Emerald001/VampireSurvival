using UnityEngine;
// Melee Weapon
public class MeleeWeapon : Weapon
{
    public override void Fire(Vector2 direction)
    {
        Debug.Log($"Swinging melee weapon for {Damage} damage!");
        // Add melee-specific logic here (e.g., detecting nearby enemies)
    }
}
