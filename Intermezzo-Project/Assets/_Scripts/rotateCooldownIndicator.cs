using UnityEngine;

public class rotateCooldownIndicator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    
    private float currentAnimationSeconds;

    void Start()
    {
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale != Vector3.zero)
        {
            float delta = Time.deltaTime / currentAnimationSeconds;

            transform.localScale -= new Vector3(delta, delta, delta);
            if (transform.localScale.y < 0f)
            {
                transform.localScale = Vector3.zero;
                spriteRenderer.enabled = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void animate(float durationSeconds)
    {
        currentAnimationSeconds = durationSeconds;
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        spriteRenderer.enabled = true;
        gameObject.SetActive(true);
    }
}
