using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int towerCost = 0;
    [SerializeField] float buildDelay = .5f;
    
    GameSession gameSession;

    void Start()
    {
        StartCoroutine(Build());
    }

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

    IEnumerator Build()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            
            foreach (Transform grandchild in child)
            {
                child.gameObject.SetActive(false);
            }
        }

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);

            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }
}
