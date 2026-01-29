using UnityEngine;
using UnityEngine.InputSystem;

public class inputScript : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void isRotate(InputAction.CallbackContext context)
    {
        var mousePos = Mouse.current.position.ReadValue();
        var hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(mousePos));

        if (!context.performed || !hit.collider) return;
        Debug.Log(hit.collider.name);
        _Rotate(hit);
    }

    private void _Rotate(RaycastHit2D pipe)
    {
        pipe.transform.Rotate(0, 0, -90);
    }

}
