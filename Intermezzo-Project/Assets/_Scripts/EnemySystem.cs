using System.Collections;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public SpriteRenderer sR;
    public bool startAlert = false;
    public bool startHostile = false;
    private Coroutine alert;
    private Coroutine hostile;
    [SerializeField]
    private bool startProgress = false;

    public GameManager gameManager;
    [SerializeField] 
    private GameObject hostilePrefab;
    [SerializeField]
    private float exposurePerSec;
    [SerializeField]
    private int getExposure;
    [SerializeField]
    private int exposureHostile;
    [SerializeField]
    private int exposureNormal;

    private void Update()
    {
        if (!startHostile)
        {
            sR.color = new Color(1f, 1f, 1f, 0.3f);
        }
        else
        {
            sR.color = Color.white;
        }
        if (startProgress) return;
           
        
        if (startAlert && !startHostile)
        {
            if (hostile != null) StopCoroutine(hostile);
            alert= StartCoroutine(alertSystem());
            return;
        }
        if (startHostile) 
        {
            if (alert != null) StopCoroutine(alert);
            gameManager.Exposure += getExposure;
            hostile = StartCoroutine (hostileSystem());
            return;
        }
    }

    IEnumerator alertSystem()
    {
        startProgress = true;
        while (startAlert && !startHostile)
        {
            gameManager.Exposure += exposureNormal;
            yield return new WaitForSeconds(exposurePerSec);
        }
        startProgress = false;
    }
    IEnumerator hostileSystem()
    {
        startProgress = true;

        while (startHostile)
        {
            gameManager.Exposure += exposureHostile;
            yield return new WaitForSeconds(exposurePerSec);
        }
        startProgress = false;
    }

    public void changeToAlert()
    {
        StopCoroutine(hostile);
    }
}
