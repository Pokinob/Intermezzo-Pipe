using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeScript : MonoBehaviour
{
    public nodeState _state;

    [SerializeField]
    private nodeCenterScript dScript;

    private void Awake()
    {
        _state = nodeState.neutral;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        nodeScript otherNode = collision.GetComponent<nodeScript>();
        nodeCenterScript otherNodeCenter = collision.GetComponentInParent<nodeCenterScript>();
        powerSource otherPowerSource = collision.GetComponent<powerSource>();

        if (_state == nodeState.neutral)
        {

            if (dScript.nodeSum > 0)
            {
                _state = nodeState.output;
            }
            else
            {
                if (otherPowerSource != null)
                {
                    _state = nodeState.input;
                    dScript.plusNodeSum();
                }
                if (otherNodeCenter == null || otherNode == null) return;

                if (otherNodeCenter.nodeSum > 0)
                {
                    if (otherNode._state == nodeState.output)
                    {
                        _state = nodeState.input;
                        dScript.plusNodeSum();
                    }
                    else if (otherNode._state == nodeState.input)
                    {
                        if (dScript.nodeSum == 0)
                        {
                            otherNode.minusNodeSystem();
                        }
                    }
                }
            }
            return;
        }
        if (_state == nodeState.input) return;
        if (_state == nodeState.output)
        {
            if (otherNode != null || otherNodeCenter != null)
                if (otherNode._state == nodeState.output)
                {
                    if (otherNodeCenter.nodeMax - otherNodeCenter.nodeSum >= dScript.nodeMax - dScript.nodeSum)
                    {
                        _state = nodeState.input;
                        dScript.plusNodeSum();
                    }
                    else
                    {
                        _state = nodeState.output;
                        return;
                    }
                }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_state == nodeState.input)
        {
            dScript.minusNodeSum();
            _state = nodeState.neutral;
            return;
        }
    }

    public void nodeReset()
    {
        if (_state == nodeState.output)
        {
            StartCoroutine(delay());
            _state = nodeState.neutral;
            return;
        }
        _state = nodeState.neutral;
    }

    public void minusNodeSystem()
    {
        dScript.minusNodeSum();
        _state = nodeState.neutral;
    }
    IEnumerator delay()
    {
        yield return new WaitUntil(() => !dScript._pause);
    }


}



public enum nodeState
{
    output,
    input,
    neutral
}