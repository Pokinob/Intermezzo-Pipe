using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class nodeCenterScript : MonoBehaviour
{
    public nodeScript[] node;
    public int nodeSum = 0;
    [SerializeField]
    private SpriteRenderer sr;
    public bool _pause = false;
    private void Update()
    {

        if (nodeSum > 0)
        {
            sr.color = Color.white;
        }
        else
        {
            if (nodeSum <= 0 && !_pause)
            {
                StartCoroutine(_reset());
                sr.color = new Color(1f, 1f, 1f, 0.3f);
            }
        }
    }

    public void minusNodeSum()
    {
        nodeSum--;
    }
    public void plusNodeSum()
    {
        nodeSum++;
    }

    IEnumerator _reset()
    {
        _pause = true;
        foreach (var n in node) 
        {
         n.nodeReset();
        }
        yield return new WaitForSeconds(0.2f);
        _pause = false;
    }
}