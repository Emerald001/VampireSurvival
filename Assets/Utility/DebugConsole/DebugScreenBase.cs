using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class DebugScreenBase<T> : MonoBehaviour
{
    // A Class that holds a bunch of Variables. Can literally be anything as long as it's a class.
    public static T varHolder;

    [SerializeField] private TextMeshProUGUI TextPrefab;
    [SerializeField] private Transform Screen;
    [SerializeField] private Vector2 TextStartPos;

    private readonly List<FieldInfo> DebugFields = new();
    private readonly List<TextMeshProUGUI> Entries = new();

    private bool IsActive;

    /// <summary>
    /// Must be called in Awake by Inheritor.
    /// </summary>
    public void Init(T objectToUse) {
        varHolder = objectToUse;

        Vector2 NextTextPos = TextStartPos;

        // We are assuming T is a Type, if not, let it crash!
        foreach (FieldInfo field in varHolder.GetType().GetFields()) {
            if (field.Name.Contains("EmptyLine")) {
                NextTextPos -= new Vector2(0, 30);
                continue;
            }

            DebugFields.Add(field);

            Entries.Add(Instantiate(TextPrefab, Screen));
            Entries[^1].transform.localPosition = NextTextPos;
            Entries[^1].text = $"{field.Name}: {field.GetValue(varHolder)}";

            NextTextPos -= new Vector2(0, 30);
        }

        Screen.gameObject.SetActive(IsActive);
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.F3))
            ToggleScreen();

        if (!IsActive)
            return;

        for (int i = 0; i < DebugFields.Count; i++)
            Entries[i].text = $"{DebugFields[i].Name}: {DebugFields[i].GetValue(varHolder)}";
    }

    private void ToggleScreen() {
        IsActive = !IsActive;

        Screen.gameObject.SetActive(IsActive);
    }
}
