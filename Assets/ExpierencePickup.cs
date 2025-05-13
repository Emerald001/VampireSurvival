using UnityEngine;

public class ExpierencePickup : MonoBehaviour
{
    public int ExpierenceAmount = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ExpierenceManager expierenceManager = other.GetComponent<ExpierenceManager>();
            if (expierenceManager != null)
            {
                expierenceManager.AddExpierence(ExpierenceAmount);
                Destroy(gameObject);
            }
        }
    }
}
