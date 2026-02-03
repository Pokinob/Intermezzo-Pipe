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

    private void Start()
    {
        SpriteRenderer sr = pipePrefab[0].GetComponent<SpriteRenderer>();
        cellSize = sr.bounds.size;

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
                } else
                {
                    GameObject nextSpawn = Instantiate(rollPipe(), nextPosition, Quaternion.identity);
                    nextSpawn.name = $"pipe {x}-{y}";
                }
            }
        }
        //camPos.transform.position = new Vector3((float)width / 2f + cellSize.x, (float)height / 2f + 0.5f, camPos.transform.position.z);
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

