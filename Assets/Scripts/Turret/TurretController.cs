using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Reference")]

    [Tooltip("Reference to Scriptable Object TurretData"), SerializeField]
    private TurretData data;

    [Tooltip("Reference of current turret head"), SerializeField]
    private Transform currentTurretHead;

    [Header("Parameters")]

    [Tooltip("Current health points of the turret."), SerializeField]
    private float currentHealth;

    /// <summary>
    /// Gets Current health points of the turret.
    /// </summary>
    public float CurrentHealth => currentHealth;

    private void Awake()
    {
        if (data == null)
        {
            Debug.LogError($"TurretController {name}: " +
                "TurretData reference is missing. Assign a TurretData asset to the data field in the Inspector.",
                this
                );
            enabled = false;
            return;
        }

        if (currentTurretHead == null)
        {
            Debug.LogError($"TurretController {name}: " +
                "Turret head reference is missing. Assign the TurretHead Transform to currentTurretHead in the Inspector.",
                this
                );
            enabled = false;
            return;
        }

        currentHealth = data.MaxHealth;
    }

}