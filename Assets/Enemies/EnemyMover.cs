using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();
    [Range(0,5)][SerializeField] float movementSpeed = 1f;

    GameSession gameSession;

    void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    void OnEnable() //Called whenever an object is enabled.
    {    
        GeneratePath();
        ReturnToStart();
        StartCoroutine(MoveEnemy());   
    }

    void GeneratePath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        foreach(Transform child in parent.transform)
        {
            Tile tile = child.GetComponent<Tile>();
            if (tile != null)
            {
                path.Add(tile);
            }
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    IEnumerator MoveEnemy()
    {
        foreach (Tile tile in path)
        {
            var startPosition = transform.position;
            var endPosition = tile.transform.position;
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
