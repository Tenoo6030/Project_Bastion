using System;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    private float rotationSpeed;
    private bool isInitialized = false;

    private Quaternion currentRotation;

    [SerializeField] private EnemyController currentTarget;


    public bool Initialize(float speed)
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

        rotationSpeed = speed;

        return isInitialized = true;
    }

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
        if (currentTarget == null)
        {
            return;
        }
        Vector3 directionToTarget = currentTarget.transform.position - transform.position;
        directionToTarget.y = 0; // Keep the turret rotation on the horizontal plane
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
