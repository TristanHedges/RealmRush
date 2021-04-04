using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maximumHealth = 5;
    [Tooltip("How much enemy maximum health will increase each time it's killed by a tower.")][SerializeField] int difficultyRamp = 1;
    int currentHealth;
    
    Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        currentHealth = maximumHealth;    
    }

    void OnParticleCollision(GameObject other)
    {
        //Debug.Log("I've been hit");
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            DeathSequence();
        }
    }

    void DeathSequence()
    {
        maximumHealth += difficultyRamp;
        enemy.RewardGold();
        gameObject.SetActive(false);
    }
}
