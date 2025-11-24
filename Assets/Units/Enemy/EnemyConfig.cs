using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/EnemyConfig", order = 1)]
public class EnemyConfig : UnitBaseStats
{
    public int Exp;

    public WeaponConfig weaponConfig;

    public Sprite enemyModel;
    public GameObject deathEffect;
}
