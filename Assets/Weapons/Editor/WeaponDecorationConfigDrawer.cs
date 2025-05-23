﻿using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(WeaponDecorationConfig))]
public class WeaponDecorationConfigDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Draw the decoration type
        SerializedProperty decorationType = property.FindPropertyRelative("decorationType");
        Rect typeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(typeRect, decorationType);

        // Draw specific fields based on the decoration type
        WeaponDecorationType type = (WeaponDecorationType)decorationType.enumValueIndex;
        Rect fieldRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, EditorGUIUtility.singleLineHeight);

        switch (type)
        {
            case WeaponDecorationType.RangedDecoration:
                SerializedProperty projectilePrefab = property.FindPropertyRelative("projectilePrefab");
                EditorGUI.PropertyField(fieldRect, projectilePrefab);
                break;

            case WeaponDecorationType.MeleeDecoration:
                SerializedProperty meleeSwingAnimation = property.FindPropertyRelative("meleeSwingAnimation");
                SerializedProperty weaponSize = property.FindPropertyRelative("weaponSize");

                // Draw weaponSize
                EditorGUI.PropertyField(fieldRect, weaponSize);
                fieldRect.y += EditorGUIUtility.singleLineHeight + 2; // Move to the next line

                // Draw meleeSwingAnimation
                EditorGUI.PropertyField(fieldRect, meleeSwingAnimation, true); // Include children
                break;

            case WeaponDecorationType.DamageUpgrade:
                SerializedProperty extraDamage = property.FindPropertyRelative("extraDamage");
                EditorGUI.PropertyField(fieldRect, extraDamage);
                break;

            case WeaponDecorationType.FireRateUpgrade:
                SerializedProperty extraFireRate = property.FindPropertyRelative("extraFireRate");
                EditorGUI.PropertyField(fieldRect, extraFireRate);
                break;

            case WeaponDecorationType.AreaOfEffectUpgrade:
                SerializedProperty areaOfEffectRadius = property.FindPropertyRelative("areaOfEffectRadius");
                EditorGUI.PropertyField(fieldRect, areaOfEffectRadius);
                break;

            case WeaponDecorationType.KnockbackUpgrade:
                SerializedProperty knockbackForce = property.FindPropertyRelative("knockbackForce");
                EditorGUI.PropertyField(fieldRect, knockbackForce);
                break;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Adjust height based on the decoration type
        SerializedProperty decorationType = property.FindPropertyRelative("decorationType");
        WeaponDecorationType type = (WeaponDecorationType)decorationType.enumValueIndex;

        int extraLines = 0;
        switch (type)
        {
            case WeaponDecorationType.RangedDecoration:
            case WeaponDecorationType.DamageUpgrade:
            case WeaponDecorationType.FireRateUpgrade:
            case WeaponDecorationType.AreaOfEffectUpgrade:
            case WeaponDecorationType.KnockbackUpgrade:
                extraLines = 1;
                break;
            case WeaponDecorationType.MeleeDecoration:
                SerializedProperty meleeSwingAnimation = property.FindPropertyRelative("meleeSwingAnimation");
                extraLines = 3 + CountChildProperties(meleeSwingAnimation); // Include child properties
                break;
                // Add cases for other decoration types as needed
        }

        return EditorGUIUtility.singleLineHeight * (1 + extraLines) + 2;
    }

    private int CountChildProperties(SerializedProperty property)
    {
        int count = 0;
        if (property.isExpanded)
        {
            SerializedProperty iterator = property.Copy();
            SerializedProperty endProperty = iterator.GetEndProperty();
            while (iterator.NextVisible(true) && !SerializedProperty.EqualContents(iterator, endProperty))
            {
                count++;
            }
        }
        return count;
    }
}
