using System.Collections;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public SpriteRenderer sR;
    public bool startAlert = false;
    public bool startHostile = false;
    private Coroutine hostile;
    [SerializeField]
    private bool startProgress = false;

    [SerializeField] 
    private GameObject hostilePrefab;
    [SerializeField]
    private float exposurePerSec;
    [SerializeField]
    private float getExposure;
    [SerializeField]
    private float exposureHostile;

    private nodeData nodeData;



    private void Start()
    {
        nodeData = gameObject.GetComponent<nodeData>();
        sR.color = new Color(1f, 1f, 1f, 0.3f);
    }

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
           
        
        if (startAlert && !startHostile && !GameManager.Instance.alreadyStart)
        {
            if (hostile != null) StopCoroutine(hostile);
            GameManager.Instance.alreadyStart = true;
            GameManager.Instance.startAlert();
            return;
        }
        if (startHostile) 
        {
            GameManager.Instance.Exposure += getExposure;
            hostile = StartCoroutine (hostileSystem());
            return;
        }
    }
    IEnumerator hostileSystem()
    {
        startProgress = true;
        while (startHostile)
        {
            if (!globalPause.instance._globalPause && !GameManager.Instance.freezeExposure)
            {
                GameManager.Instance.Exposure += exposureHostile;
                yield return new WaitForSeconds(exposurePerSec);
            }
            else
            {
                yield return null;
            }
        }
        
        startProgress = false;
    }
    
    private void OnDestroy()
    {
        if (nodeData.pipeBelow != null)
        {
            nodeData.pipeBelow.SetActive(true);
        }
    }
}
