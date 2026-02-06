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
    private bool once=false;
    [SerializeField]
    private Coroutine coroutine = null;

    private nodeData nodeData;
    private void Start()
    {
        nodeData = gameObject.GetComponent<nodeData>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
    }

    void Update()
    {
        if (!alreadyWin)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
        }
        else
        {
            spriteRenderer.color = Color.white;
            Destroy(gameObject);
        }
    }

    public void progressWin()
    {
        if (!alreadyWin && !once)
        coroutine = StartCoroutine(progStart());
    }

    public void progStop()
    {
        StopCoroutine(coroutine);
        once = false;
        Debug.Log("Data gagal terkirim");
    }

    IEnumerator progStart()
    {
        for (int i = _timer; i >= 0; i--) 
        {
            
            yield return new WaitForSeconds(1f);
            if (globalPause.instance._globalPause)
            {
                i++;
            }
            else
            {
                Debug.Log(i);
            }
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
