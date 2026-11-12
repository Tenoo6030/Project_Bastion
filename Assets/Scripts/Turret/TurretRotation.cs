using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    private float rotationSpeed;
    private float aimingTolerance;
    private bool isInitialized = false;

    [SerializeField] private EnemyController currentTarget;

    /// <summary>
    /// Initializes the turret rotation settings, 
    /// including rotation speed and aiming tolerance.
    /// </summary>
    /// <remarks>
    /// Subsequent calls are ignored and log a warning.
    /// Invalid parameters cause initialization to fail.
    /// </remarks>
    /// <param name="speed">
    /// The rotation speed in degrees per second.
    /// </param>
    /// <param name="tolerance">
    /// The maximum allowed angle deviation from the target, in degrees.
    /// </param>
    /// <returns>
    /// True if initialization is successful, false otherwise.
    /// </returns>
    public bool Initialize(float speed, float tolerance)
    {
        if (isInitialized)
        {
            Debug.LogWarning($"TurretRotation {name} : Initialize() has already been called.", this);
            return false;
        }

        if (speed < 0.1f)
        {
            Debug.LogError($"TurretRotation {name} : Invalid rotation speed.", this);
            return false;
        }

        if (tolerance < 1f)
        {
            Debug.LogError($"TurretRotation {name} : Invalid aiming tolerance.", this);
            return false;
        }

        rotationSpeed = speed;
        aimingTolerance = tolerance;

        return isInitialized = true;
    }

    /// <summary>
    /// Sets a new target for the turret to rotate towards.
    /// </summary>
    /// <param name="newTarget">
    /// The EnemyController instance representing the new target.
    /// </param>
    public void SetNewTarget(EnemyController newTarget)
    {
        if (!isInitialized)
        {
            Debug.LogError($"TurretRotation {name} : SetNewTarget() called before initialization.", this);
            return;
        }

        if (currentTarget == newTarget)
        {
            Debug.Log($"TurretRotation {name} : SetNewTarget() called with the same target. No change made.");
            return;
        }

        currentTarget = newTarget;

    }

    private void Update()
    {
        if (!isInitialized)
        {
            Debug.LogError($"TurretRotation {name} : Update() called before initialization.", this);
            return;
        }

        RotateTowardsTarget();

    }

    /// <summary>
    /// Calculates the rotation required to face the current target.
    /// </summary>
    /// <param name="targetRotation">
    /// The calculated rotation towards the target.
    /// </param>
    /// <returns>
    /// True if the target rotation was calculated successfully,
    /// false if no valid target exists.
    /// </returns>
    private bool TryGetTargetRotation(out Quaternion targetRotation)
    {
        targetRotation = Quaternion.identity;

        if (currentTarget == null)
        {
            return false;
        }

        Vector3 directionToTarget = currentTarget.transform.position - transform.position;
        directionToTarget.y = 0; // Keep the turret rotation on the horizontal plane
        targetRotation = Quaternion.LookRotation(directionToTarget);

        return true;
    }

    /// <summary>
    /// Gradually rotates the turret towards its current target.
    /// </summary>
    private void RotateTowardsTarget()
    {
        if (TryGetTargetRotation(out Quaternion targetRotation))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Checks whether the turret is aimed at its current target
    /// within the allowed aiming tolerance.
    /// </summary>
    /// <returns>
    /// True if the turret is aimed at the target,
    /// false otherwise.
    /// </returns>
    public bool IsAimedAtTarget()
    {
        if (TryGetTargetRotation(out Quaternion targetRotation))
        {
            float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
            return angleDifference <= aimingTolerance;
        }

        return false;
    }

}
