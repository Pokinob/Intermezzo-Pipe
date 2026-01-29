using UnityEngine;
using UnityEngine.InputSystem;

public class pipeScript : MonoBehaviour
{
    
    [SerializeField]
    private float[] rotations = { 0, 90, 180, 270 };

    private void Start()
    {
        int rand=Random.Range(0,rotations.Length);
        transform.eulerAngles= new Vector3(0, 0, rotations[rand]);
    }

}
