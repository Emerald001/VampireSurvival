using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    [SerializeField] private float offset = .5f;
    [SerializeField] private float colliderOffset = .4f;

    public float Damage { get; set; }

    public void SetSize(float weaponSize)
    {
        Transform visuals = transform.GetChild(0);
        if (visuals != null)
        {
            visuals.localScale = new Vector3(visuals.localScale.x, weaponSize, visuals.localScale.z);
            visuals.localPosition = new Vector3(0, weaponSize / 2 + offset, 0);
        }

        float colliderSize = weaponSize - colliderOffset;
        if (TryGetComponent<BoxCollider2D>(out var collider))
        {
            collider.size = new Vector2(0.15f, colliderSize);
            collider.offset = new Vector2(0, (offset + weaponSize) - colliderSize / 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<IDamageable>()?.TakeDamage(Damage);
    }
}
