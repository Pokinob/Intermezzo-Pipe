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
    private Coroutine coroutine = null;

    private nodeData nodeData;
    private void Start()
    {
        nodeData = gameObject.GetComponent<nodeData>();
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
        }
    }

    public void progressWin()
    {
        coroutine = StartCoroutine(progStart());
    }

    public void progStop()
    {
        StopCoroutine(coroutine);
        Debug.Log("Data gagal terkirim");
    }

    IEnumerator progStart()
    {
        for (int i = _timer; i >= 0; i--) 
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1f);
        }
        GameManager.Instance.documentCount++;
        alreadyWin = true;
        Debug.Log("Data berhasil terkirim");

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        if (nodeData.pipeBelow != null)
        {
            nodeData.pipeBelow.SetActive(true);
        }
    }
}
