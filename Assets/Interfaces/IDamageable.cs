using UnityEngine;

public interface IDamageable 
{
    public int Health { get; set; }

    public void TakeDamage(int damage);
}

public interface IBreakable
{
    public int Durability { get; set; }
    public void Break();
}

