using UnityEngine;

public class destroyTeleport : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject target = collision.transform.parent.gameObject;
        if (target != null)
        {

            if (target.CompareTag("Target") ||
            target.CompareTag("pipe"))
            {
                Destroy(target);
                return;
            }
        }
    }
}
