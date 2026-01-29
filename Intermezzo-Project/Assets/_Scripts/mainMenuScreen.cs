using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuScreen : MonoBehaviour
{
    public Button playButton;
    public Button settingsButton;

    void Start()
    {
        playButton.onClick.AddListener(ChangeToGameplayScene);
    }

    void ChangeToGameplayScene()
    {
        SceneManager.LoadSceneAsync("Assets/Scenes/InGameScene.unity");
    }
}
