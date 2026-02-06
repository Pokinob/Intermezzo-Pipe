using UnityEngine;

public class destroyTeleport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.CompareTag("Target") ||
            collision.CompareTag("Indicator"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
