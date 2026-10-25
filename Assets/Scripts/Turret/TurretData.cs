using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "Scriptable Objects/TurretData")]
public class TurretData : ScriptableObject
{
    [Tooltip("Display name of the turret."), SerializeField] 
    private string turretName;

    [Header("Reference")]

    [Tooltip("Prefab used to instantiate the turret."), SerializeField] 
    private Transform turretPrefab;

    [Header("Parameters")]

    [Tooltip("Maximum health points of the turret."), SerializeField, Range(1f, 100f)] 
    private float maxHealth;

    [Tooltip("Base damage dealt by each projectile."), SerializeField, Range(1f, 50f)] 
    private float damage;

    [Tooltip("Maximum enemy detection range in Unity units."), SerializeField, Range(0f, 50f)] 
    private float detectionRange;

    [Tooltip("Number of shots fired per second."), SerializeField]
    private float fireRate;

    [Header("Speed")]

    [Tooltip("Turret rotation speed in degrees per second."), SerializeField]
    private float rotationSpeed;

    [Tooltip("Projectile movement speed in Unity units per second."), SerializeField]
    private float projectileSpeed;

    /// <summary>
    /// Gets Display name of the turret.
    /// </summary>
    public string TurretName => turretName;

    /// <summary>
    /// Gets Prefab used to instantiate the turret.
    /// </summary>
    public Transform TurretPrefab => turretPrefab;

    /// <summary>
    /// Gets Maximum health points of the turret.
    /// </summary>
    public float MaxHealth => maxHealth;

    /// <summary>
    /// Gets Base damage dealt by each projectile.
    /// </summary>
    public float Damage => damage;

    /// <summary>
    /// Gets Maximum enemy detection range in Unity units.
    /// </summary>
    public float DetectionRange => detectionRange;

    /// <summary>
    /// Gets the number of shots fired per secend.
    /// </summary>
    public float FireRate => fireRate;

    /// <summary>
    ///Gets Turret rotation speed in degrees per second.
    /// </summary>
    public float RotationSpeed => rotationSpeed;

    /// <summary>
    /// Gets Projectile movement speed in Unity units per second.
    /// </summary>
    public float ProjectileSpeed => projectileSpeed;

    private void OnValidate()
    {
        maxHealth = Mathf.Max(1f, maxHealth);
        damage = Mathf.Max(1f, damage);
        detectionRange = Mathf.Max(0f, detectionRange);

        fireRate = Mathf.Max(0.1f, fireRate);
        rotationSpeed = Mathf.Max(0.1f, rotationSpeed);
        projectileSpeed = Mathf.Max(0.1f, projectileSpeed);
    }
}