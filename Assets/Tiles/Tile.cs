using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable = false;
    public bool IsPlaceable { get { return isPlaceable; } }
    
    Vector2Int coordinates = new Vector2Int();

    GridManager gridManager;
    Pathfinder pathfinder;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }   
    }

    void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {

            bool isSuccessful = towerPrefab.BuildTower(towerPrefab.gameObject, transform.position);
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyRecievers();
            }
        }
    }

}
