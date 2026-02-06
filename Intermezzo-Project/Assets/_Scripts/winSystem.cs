using System.Collections;
using UnityEngine;

public class winSystem : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    
    [SerializeField]
    private int _timer;
    [SerializeField]
    private bool alreadyWin=false;
    [SerializeField]
    private float colorNormal=0.3f;
    [SerializeField] 
    private bool once=false;
    [SerializeField]
    private Coroutine coroutine = null;

    private nodeData nodeData;
    private void Start()
    {
        nodeData = gameObject.GetComponent<nodeData>();
        spriteRenderer.color = new Color(1f, 1f, 1f, colorNormal);
    }

    void Update()
    {
        if (!alreadyWin) return;
        else
        {
            spriteRenderer.color = Color.white;
            Destroy(gameObject);
        }
    }

    public void progressWin()
    {
        if (!alreadyWin && !once)
        {
            once = true;
            coroutine = StartCoroutine(progStart());
        }
    }

    public void progStop()
    {
        StopCoroutine(coroutine);
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
        once = false;
        Debug.Log("Data gagal terkirim");
    }

    IEnumerator progStart()
    {
        for (int i = 1; i <= _timer; i++) 
        {
            
            if (globalPause.instance._globalPause)
            {
                i--;
            }
            else
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, colorNormal+(i*0.1f));
            }
            yield return new WaitForSeconds(1f);
        }
        GameManager.Instance.documentCount++;
        Debug.Log("Data berhasil terkirim");
        alreadyWin = true;
        yield return null;
    }
    private void OnDestroy()
    {
        if (nodeData.pipeBelow != null)
        {
            nodeData.pipeBelow.SetActive(true);
        }
    }
}
