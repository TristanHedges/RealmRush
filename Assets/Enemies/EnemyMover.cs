using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [Range(0,5)][SerializeField] float movementSpeed = 1f;
    List<Node> path = new List<Node>();

    GameSession gameSession;
    GridManager gridManager;
    Pathfinder pathfinder;

    void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void OnEnable() //Called whenever an object is enabled.
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        
        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(MoveEnemy());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetWorldPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    IEnumerator MoveEnemy()
    {
        for(int i = 1; i < path.Count; i++)
        {
            var startPosition = transform.position;
            var endPosition = gridManager.GetWorldPositionFromCoordinates(path[i].coordinates);
            var step = 0f;

            while (step < 1f)
            {
                transform.LookAt(endPosition);
                step += Time.deltaTime * movementSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, step);
                yield return new WaitForEndOfFrame();
            }
        }
        
        FinishPath();
    }

    void FinishPath()
    {
        gameSession.ReduceLives();
        gameObject.SetActive(false);
    }
}
