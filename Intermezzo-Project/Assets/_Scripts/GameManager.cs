using System.Collections;
using System.Runtime.CompilerServices;
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
    public bool freezeExposure = false;
    public int cutPipe = 0;
    public int documentCount = 0;
    [SerializeField]
    private int targetExposure;
    [SerializeField]
    private Coroutine coroutine;
    public TMP_Text documentPanel;
    [SerializeField]
    private AudioSource gameOverSound;
    [SerializeField]
    private AudioClip gameOver;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Image PropagandaPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (globalPause.instance._globalPause) return;
        if(Exposure<0) Exposure = 0;
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
        documentPanel.text = "Documents Transferred: "+documentCount.ToString();
        sliderExposure.value = Exposure;
    }

    private void UIlose()
    {
        StopAllCoroutines();
        gameOverSound.clip = gameOver;
        gameOverSound.Play();
        globalPause.instance._globalPause = true;
        gameOverPanel.SetActive(true);
        Debug.Log("Data get leaked by user");
    }
    
    public void startAlert()
    {
        StartCoroutine(alertSystem());
    }

    public void propagandaSkill()
    {
        if (coroutine != null) return;
        freezeExposure = true;
        Exposure -= 40f;
        exposureRate += 0.2f;
        StartCoroutine(durationPropaganda());
        coroutine = StartCoroutine(cooldownSkill(60f));
    }

    IEnumerator durationPropaganda()
    {
        yield return new WaitForSeconds(4f);
        freezeExposure = false;
    }

    IEnumerator cooldownSkill(float cooldown)
    {
        PropagandaPanel.color = new Color(1f, 1f, 1f, 0.3f);
        yield return new WaitForSeconds(cooldown);
        PropagandaPanel.color = Color.white;
        coroutine = null;
    }

    IEnumerator alertSystem()
    {
        while (true)
        {
            if (!globalPause.instance._globalPause && !freezeExposure)
            {
                Exposure += exposureNormal * exposureRate;
                yield return new WaitForSeconds(exposurePerSec);
            }
            else
            {
                yield return null;
            }
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
