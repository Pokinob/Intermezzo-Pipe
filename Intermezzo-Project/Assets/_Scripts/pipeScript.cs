using UnityEngine;
using UnityEngine.InputSystem;

public class pipeScript : MonoBehaviour
{
    [SerializeField]
    private Pipedefault _pipeDefault;
    [SerializeField]
    private float[] rotations = { 0, 90, 180, 270 };
    [SerializeField]
    private bool[] pipeBool; 

    private void Start()
    {
        int rand=Random.Range(0,rotations.Length);
        transform.eulerAngles= new Vector3(0, 0, rotations[rand]);
        makeBool(rand);
    }

    private void makeBool(int rand)
    {
        if (rand == 0)
        {
            
        }
        else
        {

        }
    }


}
