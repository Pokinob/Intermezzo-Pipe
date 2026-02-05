using UnityEngine;

public class openSettings : MonoBehaviour
{
    public static openSettings instance;
    public GameObject panelSettings;
    [SerializeField]
    private AudioClip button_click;

    private void Start()
    {
        instance = this;
    }
    public void openSet()
    {

        SoundFXManager.Instance.PlaySoundClip(button_click, transform, 0.5f);
        panelSettings.SetActive(!panelSettings.activeSelf);
    }
}
