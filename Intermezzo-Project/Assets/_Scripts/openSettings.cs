using UnityEngine;

public class openSettings : MonoBehaviour
{
    [SerializeField]
    private GameObject panelSettings;
    [SerializeField]
    private AudioClip button_click;

    public void openSet()
    {

        SoundFXManager.Instance.PlaySoundClip(button_click, transform, 0.5f);
        panelSettings.SetActive(!panelSettings.activeSelf);
    }
}
