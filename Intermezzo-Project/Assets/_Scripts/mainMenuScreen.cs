using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuScreen : MonoBehaviour
{
    public Button playButton;
    public Button settingsButton;
    [SerializeField]
    private AudioClip button_click;

    void Start()
    {
        playButton.onClick.AddListener(ChangeToGameplayScene);
    }

    void ChangeToGameplayScene()
    {
        SoundFXManager.Instance.PlaySoundClip(button_click, transform, 1f);
        SceneManager.LoadSceneAsync("Assets/Scenes/InGameScene.unity");
    }
}
