using UnityEngine;
using UnityEngine.SceneManagement;

public class retrySystem : MonoBehaviour
{
    [SerializeField]
    private AudioClip button_click;

    public void retryScene()
    {
        SoundFXManager.Instance.PlaySoundClip(button_click, transform, 1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
