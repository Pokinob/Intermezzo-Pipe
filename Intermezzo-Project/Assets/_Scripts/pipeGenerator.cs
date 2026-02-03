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

    private void Start()
    {
        SpriteRenderer sr = pipePrefab[0].GetComponent<SpriteRenderer>();
        cellSize = sr.bounds.size;
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
                    int rand = Random.Range(0, pipePrefab.Length);
                    GameObject nextSpawn = Instantiate(pipePrefab[rand], nextPosition, Quaternion.identity);
                    nextSpawn.name = $"pipe {x}-{y}";
                }
            }
        }
        //camPos.transform.position = new Vector3((float)width / 2f + cellSize.x, (float)height / 2f + 0.5f, camPos.transform.position.z);
    }
}
