using UnityEngine;

public class Player : Unit
{
    public override void SetData(UnitBaseStats config)
    {
        base.SetData(config);

        Health = config.health;
    }

    private void Update()
    {
        Move(new());
    }

    public override void Move(Vector3 targetPostion)
    {
        if (!GlobalNumerals.CanMove || !canMove)
            return;

        var dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        var newPosition = transform.position + dir * Stats.speed * Time.deltaTime;

        // Clamp the position to stay within the defined boundaries with padding
        var bounds = GameManager.Instance.CurrentArea.mapSize;
        float padding = 0.6f; // Padding value

        newPosition.x = Mathf.Clamp(newPosition.x, -(bounds.x / 2) + padding, (bounds.x / 2) - padding);
        newPosition.y = Mathf.Clamp(newPosition.y, -(bounds.y / 2) + padding, (bounds.y / 2) - padding);

        transform.position = newPosition;
    }

    public void EquipWeapon(WeaponConfig weapon)
    {
        weaponHolder.EquipWeapon(weapon);
    }

    public override void TakeDamage(int damage)
    {
        if (Dead)
            return;

        Health -= damage;
        unitVisuals.UpdateHealthBar(Health, Stats.health);
        CameraShake.Shake(0.1f);

        if (Health <= 0)
        {
            Die();
            return;
        }

        unitVisuals.PlayHitAnimation();
    }

    public override void Die()
    {
        canMove = false;
        Dead = true;

        Debug.Log("Player has died.");
        unitVisuals.PlayDeathAnimation(() => 
        {
            weaponHolder.Stop();

            weaponHolder.gameObject.SetActive(false);
            unitVisuals.gameObject.SetActive(false);

            EnemyManager.Instance.DoKnockback(10f, transform.position);
            GameManager.Instance.GameOver();
        });
    }

    public override void TakeKnockback(Vector2 dir, float power)
    {
        throw new System.NotImplementedException();
    }
}
