using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class nodeScript : MonoBehaviour
{
    public state _state;

    [SerializeField]
    private detectorScript dScript;

    private void Awake()
    {
        _state = state.neutral;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        nodeScript otherNode = collision.GetComponent<nodeScript>();
        detectorScript otherDs= collision.GetComponentInParent<detectorScript>();
        powerSource otherPs = collision.GetComponent<powerSource>();
        if (_state == state.neutral)
        {
            if(dScript.nodeSum > 0)
            {
                _state = state.output;
            }
            else
            {
                if (otherPs != null) 
                {
                    _state = state.input;
                    dScript.plusNodeSum();
                }
                if (otherDs == null || otherNode == null) return;

                if (otherDs.nodeSum > 0)
                {
                    if (otherNode._state == state.output)
                    {
                        _state = state.input;
                        dScript.plusNodeSum();
                    }else if( otherNode._state == state.input)
                    {
                        if(dScript.nodeSum == 0)
                        {
                            otherNode.minusNodeSystem();
                        }
                    }
                }
            }
                return;
        }

        if(_state == state.output || _state == state.input)
        {
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if( _state == state.input)
        {
            dScript.minusNodeSum();
            _state = state.neutral;
            return;
        }
    }

    public void nodeReset()
    {
        if(_state == state.output)
        {
            StartCoroutine(delay());
            _state = state.neutral;
            return;
        }
        _state = state.neutral;
    }

    public void minusNodeSystem()
    {
        dScript.minusNodeSum();
        _state = state.neutral;
    }
    IEnumerator delay()
    {
        yield return new WaitUntil(() => !dScript._pause);
    }
}



public enum state
{
    output,
    input,
    neutral
}