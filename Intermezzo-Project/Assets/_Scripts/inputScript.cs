using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputScript : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private AudioClip pipeSound;
    public bool freeze = false;
    private Camera cam;
    private Coroutine coroutine;

    private float rotateCooldownDuration = 0.25f;
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
        SoundFXManager.Instance.PlaySoundClip(pipeSound, transform, 1f);
        pipe.gameObject.transform.Rotate(0, 0, angle);
        Physics2D.SyncTransforms();

        pipe.GetComponent<pipeScript>().startIndicator(rotateCooldownDuration);
        yield return new WaitForSeconds(rotateCooldownDuration);
        //pipe.GetComponent<pipeScript>().pause = false;
        freeze = false;
    }
}
