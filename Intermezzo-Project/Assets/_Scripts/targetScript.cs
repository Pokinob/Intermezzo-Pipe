using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class targetScript : MonoBehaviour
{
    [SerializeField]
    private winSystem _winSystem;
    private targetState _state;
    private void OnTriggerStay2D(Collider2D collision)
    {
        nodeCenterScript otherNDS = collision.GetComponentInParent<nodeCenterScript>();
        if (otherNDS == null) return;
        if(_state == targetState.done) return;
        if(_state == targetState.neutral)
        {
            if(otherNDS.nodeSum > 0)
            {
                _state = targetState.process;
                _winSystem.progressWin();
            }
            return;
        }
        if(_state == targetState.process)
        {
            if(otherNDS.nodeSum <= 0)
            {
                _state = targetState.neutral;
                _winSystem.progStop();
            }
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_state == targetState.neutral) return;
        if (_state != targetState.done) 
        {
            _winSystem.progStop();
            _state = targetState.neutral;
            return;
        }
    }
}

public enum targetState
{
    neutral,
    process,
    done
}
