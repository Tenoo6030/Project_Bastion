using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "Scriptable Objects/TurretData")]
public class TurretData : ScriptableObject
{
    #region Base Parameters

    [Tooltip("Display name of the turret."), SerializeField] 
    private string turretName;
    
    [Header("Reference")]

    [Tooltip("Prefab used to instantiate the turret."), SerializeField] 
    private Transform turretPrefab;

    [Header("Parameters")]

    [Tooltip("Maximum health points of the turret."), SerializeField, Range(1f, 100f)] 
    private float maxHealth;

    #endregion

    #region Detection Parameters

     [Tooltip("Maximum enemy detection range in Unity units."), SerializeField, Range(0f, 50f)] 
    private float detectionRange;

    [Tooltip("Time between enemy detection scans in seconds."), SerializeField, Range(0.2f,1f)]
    private float detectionInterval;

    #endregion

    #region Combat Parameters

    [Tooltip("Base damage dealt by each projectile."), SerializeField, Range(1f, 50f)] 
    private float damage;

    [Tooltip("Number of shots fired per second."), SerializeField]
    private float fireRate;

    #endregion
   
    #region Speed Parameters
    [Header("Speed")]

    [Tooltip("Turret rotation speed in degrees per second."), SerializeField]
    private float rotationSpeed;

    [Tooltip("Projectile movement speed in Unity units per second."), SerializeField]
    private float projectileSpeed;
    #endregion

    #region Base Properties

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

    #endregion

    #region Detection Properties
    /// <summary>
    /// Gets Maximum enemy detection range in Unity units.
    /// </summary>
    public float DetectionRange => detectionRange;

    /// <summary>
    /// Gets the interval, in seconds, between detection scans.
    /// </summary>
    public float DetectionInterval => detectionInterval;

    #endregion

    #region Combat Properties
    /// <summary>
    /// Gets Base damage dealt by each projectile.
    /// </summary>
    public float Damage => damage;

    /// <summary>
    /// Gets the number of shots fired per secend.
    /// </summary>
    public float FireRate => fireRate;

    #endregion

    #region Speed Properties
    /// <summary>
    ///Gets Turret rotation speed in degrees per second.
    /// </summary>
    public float RotationSpeed => rotationSpeed;

    /// <summary>
    /// Gets Projectile movement speed in Unity units per second.
    /// </summary>
    public float ProjectileSpeed => projectileSpeed;

    #endregion

    #region Validate

    private void OnValidate()
    {
        maxHealth = Mathf.Max(1f, maxHealth);

        detectionRange = Mathf.Max(0f, detectionRange);
        detectionInterval = Mathf.Max(0.2f, detectionInterval);

        damage = Mathf.Max(1f, damage);
        fireRate = Mathf.Max(0.1f, fireRate);

        rotationSpeed = Mathf.Max(0.1f, rotationSpeed);
        projectileSpeed = Mathf.Max(0.1f, projectileSpeed);
    }

    #endregion

}