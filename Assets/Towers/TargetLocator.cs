using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem weaponParticleSystem;
    [SerializeField] Transform weapon;

    Transform target; 

    bool hasTarget = false;

    void Update()
    {
        SetTarget();
        AimWeapon();
    }
    void SetTarget()
    {
        if(hasTarget && !target.gameObject.activeInHierarchy) 
        { 
            hasTarget = false; 
        }

        if (hasTarget) { return; }

        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if(targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
        hasTarget = true;
    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        if(targetDistance <= range)
        {
            AttackTarget(true);
        }
        else
        {
            AttackTarget(false);
            hasTarget = false;
        }

        weapon.LookAt(target);
    }

    void AttackTarget(bool isActive)
    {
        var psEmitter = weaponParticleSystem.emission;
        psEmitter.enabled = isActive;
    }

    
}
