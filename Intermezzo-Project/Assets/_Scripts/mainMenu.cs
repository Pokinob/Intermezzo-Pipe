using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    [SerializeField]
    private AudioClip button_click;

    public void loadMenu()
    {
        SoundFXManager.Instance.PlaySoundClip(button_click, transform, 1f);
        SceneManager.LoadScene(0);
    }
}
