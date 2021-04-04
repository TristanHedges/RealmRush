using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable = false;
    public bool IsPlaceable { get { return isPlaceable; } }

    private void OnMouseDown()
    {
        if(!isPlaceable) { return; }
        bool isPlaced = towerPrefab.BuildTower(towerPrefab.gameObject, transform.position);
        isPlaceable = !isPlaced;
    }

}
