using UnityEngine;

public class pipeGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform camPos;
    [SerializeField]
    private GameObject[] pipePrefab;
    [SerializeField]
    private int width, height;
    [SerializeField]
    private Vector2 cellSize;

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
                int rand = Random.Range(0, pipePrefab.Length);
                var spawnPipe = Instantiate(pipePrefab[rand], new Vector3(x * cellSize.x, y * cellSize.y), Quaternion.identity);
                spawnPipe.name = $"pipe {x}{y}";
            }
        }
        camPos.transform.position = new Vector3((float)width / 2f + cellSize.x, (float)height / 2f + 0.5f, camPos.transform.position.z);
    }
}
