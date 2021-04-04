using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    //MOVE THIS TO EDITOR FOLDER BEFORE BUILDING

    [SerializeField] Color defaultColor = Color.gray;
    [SerializeField] Color blockedColor = Color.black;
    [SerializeField] Color exploredColor = Color.cyan;
    [SerializeField] Color inPathColor = Color.yellow;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = true;
        DisplayCoordinates();
    }

    void Update()
    {
        SetLabelColor();
        ToggleLabels();
        if (Application.isPlaying) { return; }
        DisplayCoordinates();
        UpdateObjectName();
        
    }

    void SetLabelColor()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if(node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isInPath)
        {
            label.color = inPathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
        
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = $"{coordinates.x}, {coordinates.y}";
        
    }

    void UpdateObjectName()
    {
        transform.parent.name = $"{coordinates.x}, {coordinates.y}";
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
}
