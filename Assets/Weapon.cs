using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int Damage { get; set; }
    public float Range { get; set; }
    public float FireRate { get; set; }
    protected virtual void Start()
    {
        Damage = 10;
        Range = 15f;
        FireRate = 1f;
    }
    public abstract void Fire(Vector3 targetPosition);
}
