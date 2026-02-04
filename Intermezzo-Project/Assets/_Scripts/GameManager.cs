using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool alreadyStart = false;
    [SerializeField]
    private Slider sliderExposure;
    public float exposureNormal = 5;
    public float Exposure = 0;
    public float exposurePerSec;
    public float exposureRate = 1;
    public bool delayPipe = false;
    private bool cutting = false;
    public int cutPipe = 0;
    public TMP_Text documentPanel;
    public int documentCount = 0;
    public int targetDocument = 50;
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
        documentPanel.text = "Documents Transferred: "+documentCount.ToString()+"/"+targetDocument;
        sliderExposure.value = Exposure;
    }

    private void UIlose()
    {
        StopAllCoroutines();
        Debug.Log("Data get leaked by user");
    }
    
    public void startAlert()
    {
        StartCoroutine(alertSystem());
    }

    IEnumerator alertSystem()
    {
        while (true)
        {
            Exposure += exposureNormal * exposureRate;
            yield return new WaitForSeconds(exposurePerSec);
        }
    }


    IEnumerator progressCut()
    {
        cutting = true;
        yield return new WaitForSeconds(0.1f);
        cutPipe--;
        cutting = false;
    }
}
