using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/EnemyConfig", order = 1)]
public class EnemyConfig : ScriptableObject
{
    public int health;
    public float speed;

    public float attackRange;

    public Sprite enemyModel;
    public GameObject deathEffect;
}
