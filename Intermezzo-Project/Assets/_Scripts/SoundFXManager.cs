using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;
    [SerializeField]
    private AudioSource soundFXObject;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySoundClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource source = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        
        source.clip = audioClip;

        source.volume = volume;

        source.Play();

        float clipLength = source.clip.length;

        Destroy(source.gameObject, clipLength);

    }


}
