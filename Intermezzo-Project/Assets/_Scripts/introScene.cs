using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class introScene : MonoBehaviour
{
    [SerializeField]
    private AudioClip buttonClick;
    public Button nextButton;
    public Image sceneImage;
    public TMP_Text sceneText;

    public Sprite[] imageIDs;
    public int[] imageIDSwitch;
    public string[] texts;

    private int currentSceneProgress = 0;

    private void Start()
    {
        setScene(currentSceneProgress);
        nextButton.onClick.AddListener(advanceScene);
    }

    private void advanceScene()
    {

        if (currentSceneProgress == texts.Length - 1)
        {
            SceneManager.LoadSceneAsync("Assets/Scenes/InGameScene.unity");
            return;
        }
        SoundFXManager.Instance.PlaySoundClip(buttonClick, transform, 0.5f);
        setScene(currentSceneProgress + 1);
    }
    private void setScene(int sceneProgress)
    {
        int nextSceneID = imageIDSwitch[sceneProgress];
        if (nextSceneID != -1)
        {
            sceneImage.sprite = imageIDs[nextSceneID];
        }

        sceneText.text = texts[sceneProgress];
        currentSceneProgress = sceneProgress;
    }
}
