using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Reference")]

    [Tooltip("Reference to Scriptable Object TurretData"), SerializeField]
    private TurretData data;

    [Tooltip("Reference of current turret object"), SerializeField]
    private Transform currentTurret;

    [Header("Parameters")]

    [Tooltip("Current health points of the turret."), SerializeField]
    private float currentHealth;

    [Tooltip("Base damage dealt by each projectile."), SerializeField]
    private float damage;

    [Tooltip("Maximum enemy detection range in Unity units."), SerializeField]
    private float detectionRange;

    [Tooltip("Number of shots fired per second."), SerializeField]
    private float fireRate;

    [Header("Speed")]

    [Tooltip("Turret rotation speed in degrees per second."), SerializeField]
    private float rotationSpeed;

    [Tooltip("Projectile movement speed in Unity units per second."), SerializeField]
    private float projectileSpeed;
    
    private void Awake()
    {
        if (data == null)
        {
            Debug.LogError("Data is null ");
            this.enabled = false;
            return;

        }

        if (currentTurret == null)
        {
            currentTurret = data.TurretPrefab;
        }

        currentHealth = data.MaxHealth;
        damage = data.Damage;
        detectionRange = data.DetectionRange;
        fireRate = data.FireRate;
        projectileSpeed = data.ProjectileSpeed;
        rotationSpeed = data.RotationSpeed;

    }

    private void Start()
    {
        Debug.Log(
            $"current health: {currentHealth}; " +
            $"Damage: {damage}; " +
            $"DetectionRange: {detectionRange}; " +
            $"FireRate: {fireRate}; " +
            $"ProjectileSpeed: {projectileSpeed}; " +
            $"rotationSpeed: {rotationSpeed};"
            );
    }
}