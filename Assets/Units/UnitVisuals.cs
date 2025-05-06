using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnitVisuals : MonoBehaviour
{
    public Image healthBar;
    public SpriteRenderer body;

    public GameObject deathEffect;

    private Color originalColor;
    private Vector3 originalScale;
    private Coroutine currentAnimationCoroutine;

    private void Awake()
    {
        // Store the original color and scale of the body
        if (body != null)
        {
            originalColor = body.color;
            originalScale = body.transform.localScale;
        }
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    public void PlayAttackAnimation()
    {
        if (body == null)
            return;

        // Interrupt any ongoing animation
        if (currentAnimationCoroutine != null)
            StopCoroutine(currentAnimationCoroutine);

        currentAnimationCoroutine = StartCoroutine(AttackAnimation());
    }

    private IEnumerator AttackAnimation()
    {
        // Flash white and scale down, then scale up
        body.color = Color.white;
        body.transform.localScale = originalScale * 0.8f;

        float duration = 0.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            body.transform.localScale = Vector3.Lerp(originalScale * 0.8f, originalScale * 1.2f, elapsed / duration);
            yield return null;
        }

        // Gradually return to normal color and scale
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            body.color = Color.Lerp(Color.white, originalColor, elapsed / duration);
            body.transform.localScale = Vector3.Lerp(originalScale * 1.2f, originalScale, elapsed / duration);
            yield return null;
        }

        body.color = originalColor;
        body.transform.localScale = originalScale;
        currentAnimationCoroutine = null;
    }

    public void PlayHitAnimation()
    {
        if (body == null)
            return;

        // Interrupt any ongoing animation
        if (currentAnimationCoroutine != null)
            StopCoroutine(currentAnimationCoroutine);

        currentAnimationCoroutine = StartCoroutine(HitAnimation());
    }

    private IEnumerator HitAnimation()
    {
        // Flash red and scale up slightly
        body.color = Color.red;
        body.transform.localScale = originalScale * 1.3f;

        float duration = 0.1f;
        yield return new WaitForSeconds(duration);

        // Gradually return to normal color and scale
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            body.color = Color.Lerp(Color.red, originalColor, elapsed / duration);
            body.transform.localScale = Vector3.Lerp(originalScale * 1.3f, originalScale, elapsed / duration);
            yield return null;
        }

        body.color = originalColor;
        body.transform.localScale = originalScale;
        currentAnimationCoroutine = null;
    }

    public void PlayDeathAnimation()
    {
        if (body == null || healthBar == null)
            return;

        // Interrupt any ongoing animation
        if (currentAnimationCoroutine != null)
            StopCoroutine(currentAnimationCoroutine);

        currentAnimationCoroutine = StartCoroutine(DeathAnimation());
    }

    private IEnumerator DeathAnimation()
    {
        float duration = 0.5f;
        float elapsed = 0f;

        Vector3 targetScale = Vector3.zero;
        Color healthBarColor = healthBar.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            body.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
            healthBar.color = new Color(healthBarColor.r, healthBarColor.g, healthBarColor.b, Mathf.Lerp(healthBarColor.a, 0f, elapsed / duration));
            yield return null;
        }

        body.transform.localScale = targetScale;
        healthBar.color = new Color(healthBarColor.r, healthBarColor.g, healthBarColor.b, 0f);

        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        currentAnimationCoroutine = null;
    }
}
