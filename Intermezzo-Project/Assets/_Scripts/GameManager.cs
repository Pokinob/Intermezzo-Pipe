using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int Exposure=0;
    public bool delayPipe = false;
    private bool cutting = false;
    public int cutPipe = 0;
    [SerializeField]
    private int targetExposure;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(Exposure >= targetExposure)
        {
            UIlose();
        }
        if (cutPipe > 0 && !cutting)
        {
            delayPipe = true;
            StartCoroutine(progressCut());
        }

        if (cutPipe <= 0)
        {
            delayPipe = false;
        }
    }

    private void UIlose()
    {
        Debug.Log("Data get leaked by user");
    }

    IEnumerator progressCut()
    {
        cutting = true;
        yield return new WaitForSeconds(0.1f);
        cutPipe--;
        cutting = false;
    }
}
