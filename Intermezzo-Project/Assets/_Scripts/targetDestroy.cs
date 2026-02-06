using UnityEngine;

public class targetDestroy : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject target = collision.transform.parent.gameObject;
        if (target != null)
            if (target.CompareTag("Enemy")) return;
        if (target.CompareTag("pipe"))
        {
            Destroy(target.gameObject);
            return;
        }
    }
}
