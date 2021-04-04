using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int towerCost = 0;
    
    GameSession gameSession;

    public bool BuildTower(GameObject towerPrefab, Vector3 position)
    {
        gameSession = FindObjectOfType<GameSession>();

        if(gameSession == null) { return false; }

        if (gameSession.CurrentGold >= towerCost)
        {
            Instantiate(towerPrefab, position, transform.rotation, GameObject.FindGameObjectWithTag("Towers").transform);
            gameSession.ReduceCurrentGold(towerCost);
            return true;
        }

        return false;
    }
}
