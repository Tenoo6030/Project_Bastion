using UnityEngine;

[RequireComponent(typeof(TurretDetection))]
public class TurretController : MonoBehaviour
{
    [Header("Reference")]

    [Tooltip("Reference to Scriptable Object TurretData"), SerializeField]
    private TurretData data;

    [Tooltip("Reference to TurretDetection component"), SerializeField]
    private TurretDetection detection;

    [Tooltip("Reference to TurretRotation component"), SerializeField]
    private TurretRotation rotation;

    [Header("Parameters")]

    [Tooltip("Current health points of the turret."), SerializeField]
    private float currentHealth;

    private bool initializeDone;

    /// <summary>
    /// Gets the current health points of the turret.
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

        #region Detection Initialization
        detection = GetComponent<TurretDetection>();
        if (detection == null)
        {
            Debug.LogError($"TurretController {name}: " +
                "TurretDetection reference is missing.",
                this
                );
            gameObject.SetActive(false);
            return;
        }

        initializeDone = detection.Initialize(data.DetectionRange, data.DetectionInterval);
        if (!initializeDone)
        {
            Debug.LogError($"TurretController {name}: " +
                "TurretDetection initialization failed.",
                this
                );
            enabled = false;
            return;
        }
        #endregion

        #region Rotation Initialization

        rotation = GetComponentInChildren<TurretRotation>();
        if (rotation == null)
        {
            Debug.LogError($"TurretController {name}: " +
                "TurretRotation reference is missing.",
                this
                );
            gameObject.SetActive(false);
            return;
        }

        initializeDone = rotation.Initialize(data.RotationSpeed);
        if (!initializeDone)
        {
            Debug.LogError($"TurretController {name}: " +
                "TurretRotation initialization failed.",
                this
                );
            gameObject.SetActive(false);
            return;
        }
        #endregion

        currentHealth = data.MaxHealth;
    }

    private void OnEnable()
    {
        if (initializeDone)
        {
            detection.OnTargetChanged += SetTarget;
            SetTarget(detection.CurrentTarget);
        }
    }

    private void OnDisable()
    {
        if (detection != null)
        {
            detection.OnTargetChanged -= SetTarget;
        }
    }

    /// <summary>
    /// Handles target updates received from the detection system.
    /// </summary>
    /// <param name="target">
    /// The current enemy target, or null if no target is available.
    /// </param>
    private void SetTarget(EnemyController target)
    {
        if (target != null)
        {
            Debug.Log($"{target.name}");
            rotation.SetNewTarget(target);
        }
        else
        {
            Debug.Log($"No Target");
            rotation.SetNewTarget(target);
        }
    }
}