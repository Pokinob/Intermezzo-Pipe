using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class detectorScript : MonoBehaviour
{
    public nodeScript[] node;
    public powerSource source;
    public int value = -1;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private int fixValue = -1;
    [SerializeField]
    private bool freeze =false;
    private void Update()
    {
        if (freeze) return;
        _Check();
        if(value != -1)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, 0.3f);
        }

    }

    private void _Check()
    {
        int bestValue = -1;
        powerSource bestSource = null;

        foreach (var d in node)
        {
            if (d.value == -1) continue;
            if (d.source == null) continue;
            if (bestValue == -1 || (d.value < bestValue && d.value > fixValue))
            {
                bestValue = d.value;
                bestSource = d.source;
            }
            
        }
        
        value = bestValue;
        source = bestSource;
    }

    public void _reset()
    {
        StartCoroutine(nodeReset());
    }

    IEnumerator nodeReset()
    {
        freeze = true;
        value = -1;
        source = null;
        yield return new WaitForSeconds(0.5f);
        freeze = false;
    }
}
