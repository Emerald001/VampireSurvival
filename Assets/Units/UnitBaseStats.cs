using UnityEngine;

[CreateAssetMenu(fileName = "UnitBaseStats", menuName = "ScriptableObjects/UnitBaseStats", order = 1)]
public class UnitBaseStats : ScriptableObject
{
    public string unitName;
    public Sprite unitIcon;

    public float health = 100f;
    public float speed = 5f;
}
