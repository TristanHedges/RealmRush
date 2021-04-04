using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldPerKill;

    GameSession gameSession;

    void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
    }
    
    public void RewardGold()
    {
        if (gameSession == null) { return; }
        gameSession.AddToCurrentGold(goldPerKill);
    }
}
