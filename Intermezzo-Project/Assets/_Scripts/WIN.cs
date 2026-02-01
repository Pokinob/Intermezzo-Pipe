using UnityEngine;

public class WIN : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

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

                spriteRenderer.color = Color.white;
                break;
            }
        }

        if (!alreadyWin)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
        }
    }
}
