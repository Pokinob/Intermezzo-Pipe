using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    private inputScript ingamePause;

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
    public int documentCount = 0;
    public int targetDocument = 50;
    [SerializeField]
    private int targetExposure;

    public TMP_Text documentPanel;
    [SerializeField]
    private AudioSource gameOverSound;
    [SerializeField]
    private AudioClip gameOver;
    [SerializeField]
    private GameObject gameOverPanel;

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
        gameOverSound.clip = gameOver;
        gameOverSound.Play();
        ingamePause.freeze = true;
        gameOverPanel.SetActive(true);
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
