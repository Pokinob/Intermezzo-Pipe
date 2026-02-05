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
            GameManager.Instance.Exposure+=exposureHostile;
            yield return new WaitForSeconds(exposurePerSec);
        }
        startProgress = false;
    }

}
