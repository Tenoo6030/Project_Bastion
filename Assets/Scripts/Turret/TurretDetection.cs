using System;
using UnityEngine;

public class TurretDetection : MonoBehaviour
{
    private int detectedCount;
    private float detectionRange;
    private float detectionInterval;
    private float timeSinceLastScan;
    private bool isInitialized = false;
    private Collider[] colliders = new Collider[10];
    [SerializeField] private EnemyController currentTarget;

    /// <summary>
    /// Gets the currently detected enemy target.
    /// Returns null if no target is available.
    /// </summary>
    public EnemyController CurrentTarget => currentTarget;

    /// <summary>
    /// Occurs when the current target changes.
    /// Provides the new target, or null when no target is available.
    /// </summary>
    public event Action<EnemyController> OnTargetChanged;

    /// <summary>
    /// Initializes the turret detection settings,
    /// including detection range and scan interval.
    /// </summary>
    /// <remarks>
    /// Subsequent calls are ignored and log a warning.
    /// Invalid parameters cause initialization to fail.
    /// </remarks>
    /// <param name="range">
    /// The detection range.
    /// </param>
    /// <param name="interval">
    /// The time between detection scans, in seconds.
    /// </param>
    /// <returns>
    /// True if initialization succeeds; otherwise, false.
    /// </returns>
    public bool Initialize(float range, float interval)
    {
        if (isInitialized)
        {
            Debug.LogWarning($"TurretDetection {name} : Initialize() has already been called.", this);
            return false;
        }

        if (interval < 0.2f || range < 0)
        {
            Debug.LogError($"TurretDetection {name} : Invalid detection range or scan interval.", this);
            return false;
        }

        detectionRange = range;
        detectionInterval = interval;
        timeSinceLastScan = interval;

        return isInitialized = true;
    }

    private void Update()
    {
        if (!isInitialized)
        {
            return;
        }

        timeSinceLastScan += Time.deltaTime;

        if (timeSinceLastScan >= detectionInterval)
        {
            ScanForTargets();
            timeSinceLastScan = 0f;
        }

    }

    /// <summary>
    /// Scans the detection area for enemies and selects the nearest target.
    /// Automatically expands the collider buffer when necessary.
    /// Notifies subscribers when the current target changes.
    /// </summary>
    private void ScanForTargets()
    {
        bool needsRescan;

        // Expand the collider buffer and rescan until all detected colliders fit.
        do
        {
            needsRescan = false;
            detectedCount = Physics.OverlapSphereNonAlloc(transform.position, detectionRange, colliders);
            if (detectedCount == colliders.Length)
            {
                colliders = new Collider[colliders.Length * 2];
                needsRescan = true;
            }
        }
        while (needsRescan);

        EnemyController closestEnemy = null;
        float closestDistanceSqr = float.PositiveInfinity;

        // Find the nearest valid enemy using squared distances.
        for (int i = 0; i < detectedCount; i++)
        {
            EnemyController enemy = colliders[i].GetComponentInParent<EnemyController>();
            if (enemy == null)
            {
                continue;
            }

            float distanceSqr = (enemy.transform.position - transform.position).sqrMagnitude;

            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestEnemy = enemy;
            }

        }

        // Compare actual references to handle destroyed Unity objects correctly.
        // Notify subscribers only when the target changes.
        if (!ReferenceEquals(closestEnemy, CurrentTarget))
        {
            currentTarget = closestEnemy;
            OnTargetChanged?.Invoke(CurrentTarget);
        }
    }
}
