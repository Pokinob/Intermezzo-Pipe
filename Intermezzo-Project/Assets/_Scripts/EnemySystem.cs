using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

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
                spriteRenderer.color = Color.white;
                break;
            }
        }

        if (!alreadyLose)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
        }
    }
}
