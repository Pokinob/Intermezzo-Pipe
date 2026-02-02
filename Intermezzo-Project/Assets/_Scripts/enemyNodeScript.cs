using UnityEngine;

public class enemyNodeScript : MonoBehaviour
{

    [SerializeField]
    private EnemySystem _enemySystem;
    [SerializeField]
    private enemyState state;
    private void OnTriggerStay2D(Collider2D collision)
    {
        nodeCenterScript NDS = collision.GetComponentInParent<nodeCenterScript>();
        if (NDS == null) return;
        if(state == enemyState.neutral)
        {
            if(NDS.nodeSum > 0)
            {
                _enemySystem.startHostile = true;
                _enemySystem.startAlert = true;
                state=enemyState.hostile;
                return;
            }
        }
        if(state == enemyState.alert)
        {
            if (NDS.nodeSum > 0)
            {
                state =enemyState.hostile;
                return;
            }
        }

        if (state == enemyState.hostile) 
        {

            if (NDS.nodeSum > 0)
            {
                //Start Hostile
                _enemySystem.startHostile=true;
            }
            else
            {
                _enemySystem.startHostile=false;
                state = enemyState.alert;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(state == enemyState.hostile)
        {
            _enemySystem.startHostile = false;
            state = enemyState.alert;
            return;
        } 
            
    }
}


public enum enemyState
{
    neutral,
    alert,
    hostile
}

