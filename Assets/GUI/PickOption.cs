using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PickOption<T> : MonoBehaviour
{
    [SerializeField] protected Button button;
    [SerializeField] protected Image icon;
    [SerializeField] protected TextMeshProUGUI nameText;
    protected T item;

    public virtual void SetData(T item)
    {
        this.item = item;
        button.onClick.AddListener(OnClick);
    }

    protected abstract void OnClick();
}
