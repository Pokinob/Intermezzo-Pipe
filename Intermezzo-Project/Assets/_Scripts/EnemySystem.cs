using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    [SerializeField]
    private WinScript[] winScript;
    [SerializeField]
    private bool alreadyLose = false;
    void Update()
    {
        if (alreadyLose) return;
        foreach (WinScript w in winScript)
        {
            if (w.value > 0)
            {
                Debug.Log("WAIT WHAT DATA BOCOR");
                alreadyLose = true;
                w.alreadyWin = true;
            }
        }
    }
}
