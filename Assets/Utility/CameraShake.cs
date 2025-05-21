using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeFrequency = 20f;

    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    public static void Shake(float intensity)
    {
        if (Instance != null)
            Instance.StartShake(intensity);
    }

    private void StartShake(float intensity)
    {
        originalPosition = transform.localPosition;

        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(ShakeCoroutine(intensity));
    }

    private System.Collections.IEnumerator ShakeCoroutine(float intensity)
    {
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * intensity;
            float y = Random.Range(-1f, 1f) * intensity;
            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;
        shakeCoroutine = null;
    }
}
