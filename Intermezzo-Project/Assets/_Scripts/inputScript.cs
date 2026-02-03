using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;
using static UnityEngine.Rendering.DebugUI;

public class inputScript : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel;

    public bool freeze = false;
    private Camera cam;
    private Coroutine coroutine;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void openMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

    public void esc(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            openMenu();
        }
    }



    public void tryRotate(InputAction.CallbackContext context)
    {
        if (freeze) return;
        var mousePos = Mouse.current.position.ReadValue();
        var hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(mousePos));

        if (!context.performed || !hit.collider) return;

        Collider2D colObj;

        if (hit.collider.tag == "detector")
        {
            colObj = hit.collider.gameObject.GetComponentInParent<Collider2D>();
        }
        else
        {
            colObj = hit.collider.GetComponent<Collider2D>();
        }

        //Debug.Log(hit.collider.name);
        switch (context.action.name)
        {
            case "RotateClockwise":
                coroutine = StartCoroutine(_Rotate(colObj, -90));
                break;

            //case "RotateCounterclockwise":
            //    coroutine = StartCoroutine(_Rotate(colObj, 90));
            //    break;
        }
    }

    IEnumerator _Rotate(Collider2D pipe, int angle)
    {
        freeze = true;
        pipe.gameObject.transform.Rotate(0, 0, angle);
        //pipe.GetComponent<pipeScript>().pause = true;
        Physics2D.SyncTransforms();
        yield return new WaitForSeconds(0.5f);
        //pipe.GetComponent<pipeScript>().pause = false;
        freeze = false;
    }
}
