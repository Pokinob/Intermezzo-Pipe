using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class volumeSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer myMixer;
    [SerializeField]
    private Slider musicSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            loadVolume();
        }
        else
        {
            SetMusicVolume();
        }
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Master", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    
    private void loadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        SetMusicVolume();
    }
}
