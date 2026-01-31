using UnityEngine;

public class WIN : MonoBehaviour
{
    [SerializeField]
    private WinScript[] winScript;
    [SerializeField]
    private bool alreadyWin=false;
    void Update()
    {
        if(alreadyWin) return;
        foreach (WinScript w in winScript) 
        {
            if(w.value >0)
            {
                Debug.Log("Win");
                alreadyWin = true;
                w.alreadyWin = true;
            }
        }
    }
}
