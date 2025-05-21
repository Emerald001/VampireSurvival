using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPicker : MonoBehaviour
{
    public static OptionPicker Instance { get; private set; }
    private void Awake() => Instance = this;

    [SerializeField] private PickOption<WeaponConfig> weaponPrefab;
    [SerializeField] private PickOption<ProjectileConfig> projectilePrefab;
    [SerializeField] private PickOption<WeaponDecorationConfig> weaponDecorationPrefab;
    [SerializeField] private PickOption<PlayerDecorationConfig> playerDecorationPrefab;
    [SerializeField] private PickOption<PlayerConfig> playerPrefab;
    [SerializeField] private Transform content;

    [SerializeField] private float initialDelay = 0.5f;
    [SerializeField] private float popInDelay = 0.05f;
    [SerializeField] private float popInDuration = 0.15f;

    private List<GameObject> spawnedObjects = new();

    public void SetOptions<T>(List<T> values)
    {
        content.gameObject.SetActive(true);

        spawnedObjects.Clear();

        void SpawnItems<P, C>(PickOption<C> prefab) where C : class
        {
            foreach (var item in values)
            {
                var option = Instantiate(prefab, content);
                option.transform.localScale = Vector3.one; // Ensure normal scale for layout
                option.SetData(item as C);
                spawnedObjects.Add(option.gameObject);
            }
        }

        switch (typeof(T).Name)
        {
            case nameof(WeaponConfig):
                SpawnItems<PickOption<WeaponConfig>, WeaponConfig>(weaponPrefab);
                break;
            case nameof(ProjectileConfig):
                SpawnItems<PickOption<ProjectileConfig>, ProjectileConfig>(projectilePrefab);
                break;
            case nameof(WeaponDecorationConfig):
                SpawnItems<PickOption<WeaponDecorationConfig>, WeaponDecorationConfig>(weaponDecorationPrefab);
                break;
            case nameof(PlayerDecorationConfig):
                SpawnItems<PickOption<PlayerDecorationConfig>, PlayerDecorationConfig>(playerDecorationPrefab);
                break;
            case nameof(PlayerConfig):
                SpawnItems<PickOption<PlayerConfig>, PlayerConfig>(playerPrefab);
                break;
            default:
                Debug.LogError("Unsupported type: " + typeof(T));
                break;
        }

        // Force layout rebuild so ContentSizeFitter updates
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)content);

        // Start coroutine for pop-in animation
        StartCoroutine(AnimatePopInCoroutine(spawnedObjects));
    }

    private IEnumerator AnimatePopInCoroutine(List<GameObject> objects)
    {
        for (int i = 0; i < objects.Count; i++)
            objects[i].SetActive(false);

        yield return new WaitForSeconds(initialDelay);

        for (int i = 0; i < objects.Count; i++)
        {
            var obj = objects[i];
            obj.transform.localScale = Vector3.zero;
            obj.SetActive(true);

            LeanTween.scale(obj, Vector3.one, popInDuration)
                .setEase(LeanTweenType.easeOutBack);
            yield return new WaitForSeconds(popInDelay);
        }
    }

    public void ClearOptions()
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        spawnedObjects.Clear();

        content.gameObject.SetActive(false);
    }
}

public class PlayerDecorationConfig
{
    // Add properties for player decoration configuration
}

public class PlayerConfig
{
    // Add properties for player configuration
}
