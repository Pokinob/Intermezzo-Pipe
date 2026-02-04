using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pipeGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform camPos;

    [SerializeField]
    private GameObject[] pipePrefab;
    [SerializeField]
    private GameObject powerNodePrefab;
    [SerializeField]
    private GameObject destinationNodePrefab;
    [SerializeField]
    private GameObject enemyNodePrefab;

    [SerializeField]
    private int width, height;
    [SerializeField]
    private Vector2 cellSize;
    [SerializeField]
    private Vector2 cellPositionOffset;

    [SerializeField]
    private Vector2[] powerNodePositions;

    [SerializeField]
    private int[] pipeWeights;

    private int totalWeights;
    private List<Vector2> unavailableNodePositions;

    private void Start()
    {
        SpriteRenderer sr = pipePrefab[0].GetComponent<SpriteRenderer>();
        cellSize = sr.bounds.size;
        unavailableNodePositions = new List<Vector2>();
    
        foreach (int weight in pipeWeights)
        {
            totalWeights += weight;
        }

        Generate();
    }

    void Generate()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 nextPosition = new Vector3(
                    (x * cellSize.x) + 0.5f + cellPositionOffset.x,
                    (y * cellSize.y) + 0.5f + cellPositionOffset.y
                );

                bool isPowerNode = false;
                foreach (Vector2 nodePositions in powerNodePositions)
                {
                    if (x == nodePositions.x && y == nodePositions.y)
                    {
                        isPowerNode = true;
                        break;
                    }
                }

                if (isPowerNode)
                {
                    GameObject nextSpawn = Instantiate(powerNodePrefab, nextPosition, Quaternion.identity);
                    nextSpawn.name = $"power {x}-{y}";
                }
                else
                {
                    GameObject nextSpawn = Instantiate(rollPipe(), nextPosition, Quaternion.identity);
                    nextSpawn.name = $"pipe {x}-{y}";
                }
            }
        }

        foreach (Vector2 nodePosition in powerNodePositions)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    unavailableNodePositions.Add(nodePosition + new Vector2(x, y));
                }
            }
        }
        
        for (int i = 0; i < 2; i++)
        {
            generateTargetNode();
        }
        //camPos.transform.position = new Vector3((float)width / 2f + cellSize.x, (float)height / 2f + 0.5f, camPos.transform.position.z);
    }

    private void generateTargetNode()
    {
        Vector2 nextPosition = getAvailableNodePosition();
        Vector2 actualPosition = nextPosition + new Vector2(0.5f, 0.5f) + cellPositionOffset;

        GameObject nextSpawn = Instantiate(destinationNodePrefab, actualPosition, Quaternion.identity);
        nextSpawn.name = $"target {nextPosition.x}-{nextPosition.y}";

        GameObject overridenPipe = GameObject.Find($"pipe {nextPosition.x}-{nextPosition.y}");
        overridenPipe.SetActive( false );
    }

    private Vector2 getAvailableNodePosition()
    {
        while (true)
        {
            Vector2 newPosition = new Vector2(
                Random.Range(0, width),
                Random.Range(0, height)
            );

            bool isFound = true;
            foreach (Vector2 unavailablePosition in unavailableNodePositions)
            {
                if (newPosition == unavailablePosition)
                {
                    isFound = false;
                    break;
                }
            }

            if (isFound)
            {
                // offset
                return newPosition;
            }
        }
    }

    private GameObject rollPipe()
    {
        int roll = Random.Range(0, totalWeights);
        int idx = 0;
        int weightSum = 0;
        foreach (int weight in pipeWeights)
        {
            weightSum += weight;
            if (weightSum > roll)
            {
                break;
            }

            idx++;
        }

        return pipePrefab[idx];
    }
}
