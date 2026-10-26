using UnityEngine;

public class TurretDetection : MonoBehaviour
{
    
    private float detectionRange;

    private float detectionInterval;

    private bool isInitialized = false;

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

        if (interval <0.2f || range < 0)
        {
            Debug.LogError($"TurretDetection {name} : Invalid detection range or scan interval.", this);
            return false;
        } 

        detectionRange = range;
        detectionInterval = interval;

        return isInitialized = true;
    }

    private void Update()
    {
        if (!isInitialized)
        {
            return;
        }

    }
}
