using UnityEngine;

public class AudioManager : BaseManager<AudioManager>
{
    [SerializeField]
    private AudioChannelSO audioChannelSO;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioChannelSO.OnPlayAudio += PlayAudio;
    }

    private void PlayAudio(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
