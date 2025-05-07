using UnityEditor;

[CustomEditor(typeof(WeaponConfig))]
public class WeaponConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        WeaponConfig config = (WeaponConfig)target;

        // Draw default fields
        EditorGUILayout.LabelField("Weapon Configuration", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("    Weapon Visuals");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("weaponName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("description"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("icon"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("weaponPrefab"));

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("    Weapon Stats");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fireRate"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("attackRange"));

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("    Initial Decorations");
        // Draw the weapon decorations list
        SerializedProperty decorations = serializedObject.FindProperty("weaponDecorations");
        EditorGUILayout.PropertyField(decorations, true);

        serializedObject.ApplyModifiedProperties();
    }
}
