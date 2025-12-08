using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPicker : MonoBehaviour
{
    [SerializeField] private PickOption<UnitBaseStats> playerPrefab;
    [SerializeField] private PickOption<WeaponConfig> weaponPrefab;
    [SerializeField] private PickOption<ProjectileConfig> projectilePrefab;
    [SerializeField] private PickOption<WeaponDecorationConfig> weaponDecorationPrefab;
    [SerializeField] private PickOption<PlayerDecorationConfig> playerDecorationPrefab;
    [SerializeField] private Transform content;

    [Header("Animation Settings")]
    [SerializeField] private float initialDelay = 0.5f;
    [SerializeField] private float popInDelay = 0.05f;
    [SerializeField] private float popInDuration = 0.15f;

    [Header("Selection")]
    [SerializeField] private Color selectedColor = Color.gray;
    [SerializeField] private Color normalColor = Color.white;

    private List<GameObject> spawnedObjects = new();
    private bool allowSelection = false;
    private object selectedItem;
    private int selectedIndex = -1;

    public void SetOptions<T>(List<T> values, System.Action<T> onSelect, bool allowSelection = false)
    {
        content.gameObject.SetActive(true);
        this.allowSelection = allowSelection;
        selectedItem = null;

        spawnedObjects.Clear();

        void SpawnItems<P, C>(PickOption<C> prefab) where C : class
        {
            int index = 0;
            foreach (var item in values)
            {
                int currentIndex = index;
                index++;

                var option = Instantiate(prefab, content);
                option.transform.localScale = Vector3.one;
                option.SetData(item as C, currentIndex);
                
                option.onSelected = (selected) =>
                {
                    if (this.allowSelection)
                        SetSelectedItem<C>(selected, currentIndex);

                    onSelect?.Invoke(item);
                };

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
            case nameof(UnitBaseStats):
                SpawnItems<PickOption<UnitBaseStats>, UnitBaseStats>(playerPrefab);
                break;
            default:
                Debug.LogError("Unsupported type: " + typeof(T));
                break;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)content);
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
        selectedItem = null;

        content.gameObject.SetActive(false);
    }

    public void SetSelectedItem<C>(object item, int index)
    {
        Debug.Log($"Item selected: {item}");

        selectedItem = item;
        selectedIndex = index;
        
        foreach (var obj in spawnedObjects)
        {
            var optionInList = obj.GetComponent<PickOption<C>>(); 
            optionInList.backgroundImage.color = (optionInList.Index == index) ? selectedColor : normalColor;
        }
    }

    public T GetSelectedItem<T>() where T : class
    {
        return selectedItem as T;
    }
}
