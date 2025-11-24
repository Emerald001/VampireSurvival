public interface IDamageable 
{
    public float Health { get; set; }
    public bool Dead { get; set; }

    public void TakeDamage(float damage);
}

public interface IBreakable
{
    public int Durability { get; set; }
    public void Break();
}

