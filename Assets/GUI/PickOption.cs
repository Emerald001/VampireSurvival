using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PickOption<T> : MonoBehaviour
{
    [SerializeField] protected Button button;
    [SerializeField] protected Image icon;
    [SerializeField] protected TextMeshProUGUI nameText;
    public Image backgroundImage;
    protected T item;

    public System.Action<T> onSelected;
    public int Index { get; set; }

    public virtual void SetData(T item, int index)
    {
        this.item = item;
        Index = index;
        button.onClick.AddListener(OnClick);
    }

    protected virtual void OnClick()
    {
        Debug.Log("Option clicked: " + item);
        onSelected?.Invoke(item);
    }
}
