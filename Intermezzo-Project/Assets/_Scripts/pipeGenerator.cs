using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class pipeGenerator : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    
    public static pipeGenerator Instance;

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
    private GameObject indicatorNodePrefab;

    [SerializeField]
    private int width, height;
    [SerializeField]
    private int maxEnemy = 2;
    public int bonusEnemy = 0;
    [SerializeField]
    private Vector2 cellSize;
    [SerializeField]
    private Vector2 cellPositionOffset;

    [SerializeField]
    private Vector2[] powerNodePositions;

    [SerializeField]
    private int[] pipeWeights;

    [SerializeField]
    private int enemySpawnTimeSeconds;

    [SerializeField]
    private int targetSpawnTimeSeconds; 

    private int totalWeights;
    private List<Vector2> unavailableNodePositions;
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

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

        StartCoroutine(loopSpawnEnemy());
        StartCoroutine(loopSpawnTarget());
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
            generateNodeAtRandomPosition("target", destinationNodePrefab);
        }
        //camPos.transform.position = new Vector3((float)width / 2f + cellSize.x, (float)height / 2f + 0.5f, camPos.transform.position.z);
    }

    private GameObject generateNodeAtRandomPosition(string nodeName, GameObject node)
    {
        Vector2 nextPosition = getNodeAvailableRandomPosition();
        unavailableNodePositions.Add(nextPosition);

        return generateNodeAtUnmarked(nodeName, node, nextPosition);
    }

    private GameObject generateNodeAtUnmarked(string nodeName, GameObject node, Vector2 at)
    {
        GameObject newNode = generateObjectAtGrid(nodeName, node, at);

        GameObject overridenPipe = GameObject.Find($"pipe {at.x}-{at.y}");

        if (overridenPipe != null)
        {
            overridenPipe.SetActive(false);
            newNode.GetComponent<nodeData>().pipeBelow = overridenPipe;
        }
        return newNode;
    }

    private GameObject generateObjectAtGrid(string objName, GameObject obj, Vector2 at)
    {
        Vector2 actualPosition = at + new Vector2(0.5f, 0.5f) + cellPositionOffset;

        GameObject spawnedNode = Instantiate(obj, actualPosition, Quaternion.identity);
        spawnedNode.name = $"{objName} {at.x}-{at.y}";

        return spawnedNode;
    }


    private Vector2 getNodeAvailableRandomPosition()
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

    void ClearTile(Vector2 at)
    {
        GameObject target = GameObject.Find($"target {at.x}-{at.y}");
        if (target != null)
            Destroy(target);

        GameObject enemy = GameObject.Find($"enemy {at.x}-{at.y}");
        if (enemy != null)
            Destroy(enemy);

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

    IEnumerator loopSpawnEnemy()
    {
        while (true)
        {
            if (!globalPause.instance._globalPause)
            {
                Vector2 nextEnemyPosition = getNodeAvailableRandomPosition();
                GameObject indicator = generateObjectAtGrid("indicator", indicatorNodePrefab, nextEnemyPosition);
                yield return new WaitForSeconds(enemySpawnTimeSeconds);

                if (activeEnemies.Count < maxEnemy + bonusEnemy)
                {
                    unavailableNodePositions.Add(nextEnemyPosition);
                    ClearTile(nextEnemyPosition);
                    GameObject newNode = generateNodeAtUnmarked("enemy", enemyNodePrefab, nextEnemyPosition);
                    activeEnemies.Add(newNode);
                    GameObject.Destroy(indicator);
                }
                else
                {
                    yield return null;
                    int idx = Random.Range(0, activeEnemies.Count);
                    TeleportEnemy(activeEnemies[idx], nextEnemyPosition);
                    GameObject.Destroy(indicator);
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    void TeleportEnemy(GameObject enemy, Vector2 newPos)
    {
        if (enemy == null) return;

        nodeData nd = enemy.GetComponent<nodeData>();
        if (nd == null) return;

        if (nd.pipeBelow != null) nd.pipeBelow.SetActive(true);

        ClearTile(newPos);

        enemy.transform.position = newPos + new Vector2(0.5f, 0.5f) + cellPositionOffset;

        GameObject overridenPipe = GameObject.Find($"pipe {newPos.x}-{newPos.y}");
        if (overridenPipe == null) return;

        overridenPipe.SetActive(false);
        nd.pipeBelow = overridenPipe;
    }



    IEnumerator loopSpawnTarget()
    {
        while (true)
        {
            if (!globalPause.instance._globalPause)
            {
                yield return new WaitForSeconds(targetSpawnTimeSeconds);
                generateNodeAtRandomPosition("target", destinationNodePrefab);
                generateNodeAtRandomPosition("target", destinationNodePrefab);
            }
            else
            {
                yield return null;
            }
        }
    }

}
