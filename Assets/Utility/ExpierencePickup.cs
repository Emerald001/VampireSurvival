using UnityEngine;

public class ExpierencePickup : MonoBehaviour
{
    public int ExpierenceAmount { get; private set; }

    public void SetData(int expierenceAmount)
    {
        ExpierenceAmount = expierenceAmount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ExpierenceManager.Instance.AddExpierence(ExpierenceAmount);
            Destroy(gameObject);
        }
    }
}
