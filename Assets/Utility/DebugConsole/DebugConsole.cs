using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DebugConsole : MonoBehaviour
{
    [SerializeField] private GameObject DebugScreenObject;
    [SerializeField] private TMP_InputField InputField;
    [SerializeField] private TextMeshProUGUI AutoCompleteArea;
    [SerializeField] private ConsoleCommands ConsoleCommands;

    private readonly List<string> autoCompletes = new();

    private int autoCompleteIndex = 0;
    private bool IsActive;

    private void Start() {
        InputField.onValueChanged.AddListener(AutoCompleteEntries);

        ConsoleCommands.InitDict();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F2))
            ToggleScreen();

        if (!IsActive)
            return;

        if (Input.GetKeyDown(KeyCode.Return)) {
            if (!InputField.text.Contains(' '))
                throw new Exception("Must be two Bits");

            var split = InputField.text.Split(' ');
            if (ConsoleCommands.actions.ContainsKey(split[0]))
                if (float.TryParse(split[1], out float value))
                    ConsoleCommands.actions[split[0]].Invoke(value);

            ToggleScreen();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
            AutoComplete();
    }

    private void ToggleScreen() {
        IsActive = !IsActive;

        DebugScreenObject.SetActive(IsActive);

        if (IsActive)
            InputField.Select();
        else {
            InputField.ReleaseSelection();
            InputField.text = "";
            AutoCompleteArea.text = "";
        }
    }

    private void AutoCompleteEntries(string value) {
        autoCompletes.Clear();
        autoCompleteIndex = 0;

        if (value == "") {
            AutoCompleteArea.text = "";
            return;
        }

        foreach (var item in ConsoleCommands.actions.Keys.ToList())
            if (item.ToLower().Contains(value.ToLower()))
                autoCompletes.Add(item);

        string output = "";
        foreach (var item in autoCompletes)
            output += item + "\n";
        AutoCompleteArea.text = output;
    }

    private void AutoComplete() {
        InputField.onValueChanged.RemoveAllListeners();

        InputField.text = autoCompletes[autoCompleteIndex];
        InputField.MoveToEndOfLine(false, false);

        autoCompleteIndex++;

        if (autoCompleteIndex > autoCompletes.Count - 1)
            autoCompleteIndex = 0;

        InputField.onValueChanged.AddListener(AutoCompleteEntries);
    }
}

/// <summary>
/// Derive from this and put it on an object.
/// It's not super clean, but it will work for now.
/// </summary>
public abstract class ConsoleCommands : MonoBehaviour
{
    public Dictionary<string, Action<float>> actions = new();

    public abstract void InitDict();
}
