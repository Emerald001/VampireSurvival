using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitVisuals : MonoBehaviour
{
    public Image healthBar;
    public SpriteRenderer body;

    public GameObject deathEffect;

    private Color originalColor;
    private Vector3 originalScale;
    private bool dying;

    private void Awake()
    {
        // Store the original color and scale of the body
        if (body != null)
        {
            originalColor = body.color;
            originalScale = body.transform.localScale;
        }
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void PlayAttackAnimation()
    {
        if (body == null || dying)
            return;

        // Flash white and scale down, then scale up
        body.color = Color.white;
        LeanTween.scale(body.gameObject, originalScale * 0.8f, 0.1f).setOnComplete(() =>
        {
            LeanTween.scale(body.gameObject, originalScale * 1.2f, 0.1f).setOnComplete(() =>
            {
                LeanTween.scale(body.gameObject, originalScale, 0.1f);
                LeanTween.value(gameObject, Color.white, originalColor, 0.1f).setOnUpdate((Color color) =>
                {
                    body.color = color;
                });
            });
        });
    }

    public void PlayHitAnimation()
    {
        if (body == null || dying)
            return;

        // Flash red and scale up slightly
        body.color = Color.red;
        LeanTween.scale(body.gameObject, originalScale * 1.3f, 0.1f).setOnComplete(() =>
        {
            LeanTween.scale(body.gameObject, originalScale, 0.1f);
            LeanTween.value(gameObject, Color.red, originalColor, 0.1f).setOnUpdate((Color color) =>
            {
                body.color = color;
            });
        });
    }

    public void PlayDeathAnimation(Action onComplete)
    {
        if (body == null || healthBar == null)
            return;

        dying = true;

        // Scale body to zero and fade out health bar
        LeanTween.scale(body.gameObject, Vector3.zero, 0.5f);
        LeanTween.value(gameObject, healthBar.color.a, 0f, 0.5f).setOnUpdate((float alpha) =>
        {
            Color healthBarColor = healthBar.color;
            healthBar.color = new Color(healthBarColor.r, healthBarColor.g, healthBarColor.b, alpha);
        }).setOnComplete(() =>
        {
            if (deathEffect != null)
                Instantiate(deathEffect, transform.position, Quaternion.identity);

            onComplete?.Invoke();
        });
    }
}
