using UnityEngine;
using UnityEngine.InputSystem;

public class pipeScript : MonoBehaviour
{
    [SerializeField]
    private float[] rotations = { 0, 90, 180, 270};

    public bool pause = false;
    private void Start()
    {
        int rand=Random.Range(0,rotations.Length);
        transform.Rotate(0, 0, rotations[rand]);
        Physics2D.SyncTransforms();
    }

    private void Update()
    {
        if (pause) return;
    }
}
