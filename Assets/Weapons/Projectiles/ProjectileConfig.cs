using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "ScriptableObjects/ProjectileConfig", order = 4)]
public class ProjectileConfig : ScriptableObject
{
    [Header("Basic Info")]
    public string projectileName;
    public Sprite icon;
    [TextArea(2, 4)]
    public string description;

    [Header("Base Stats")]
    public float baseDamage = 10f;
    public float speed = 10f;
    public float lifetime = 5f;
    public float size = 1f;

    [Header("Behavior")]
    public bool piercing = false;
    public int maxPierceCount = 1;
    public bool homing = false;
    public float homingStrength = 5f;

    [Header("Visual")]
    public GameObject projectilePrefab;
    public TrailRenderer trailPrefab;
    public GameObject hitEffect;

    [Header("Decorations")]
    public List<ProjectileDecorationConfig> decorations = new();
}
