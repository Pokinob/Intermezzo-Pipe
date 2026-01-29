using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class inputScript : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void tryRotate(InputAction.CallbackContext context)
    {
        var mousePos = Mouse.current.position.ReadValue();
        var hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(mousePos));
       
        if (!context.performed || !hit.collider) return;
        Debug.Log(hit.collider.name);
        switch (context.action.name)
        {
            case "RotateClockwise":
                _Rotate(hit, -90);
                break;

            case "RotateCounterclockwise":
                _Rotate(hit, 90);
                break;
        }
    }

    private void _Rotate(RaycastHit2D pipe, int angle)
    {
        pipe.transform.Rotate(0, 0, angle);
    }

}
