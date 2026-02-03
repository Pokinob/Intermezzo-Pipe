using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class nodeCenterScript : MonoBehaviour
{
    public nodeScript[] node;
    public bool poweredFromSource = false;
    public int nodeSum = 0;
    public int nodeMax;
    [SerializeField]
    private SpriteRenderer sr;
    public bool _pause = false;

    private void Start()
    {
        nodeMax = node.Length;
    }
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
        yield return new WaitForSeconds(0.1f);
        _pause = false;
    }
}