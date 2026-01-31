using Unity.VisualScripting;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    public bool alreadyWin = false;
    public int value = -1;
    private void OnTriggerStay2D(Collider2D collision)
    {
        detectorScript ds = collision.GetComponentInParent<detectorScript>();
        if (alreadyWin) return;
        if(ds == null) return;
        value= ds.value;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        value = -1;
    }
}
