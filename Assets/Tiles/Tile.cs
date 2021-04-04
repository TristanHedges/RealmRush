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

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
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
        if(!isPlaceable) { return; }
        bool isPlaced = towerPrefab.BuildTower(towerPrefab.gameObject, transform.position);
        isPlaceable = !isPlaced;
    }

}
