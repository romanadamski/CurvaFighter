using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioChannelSO audioChannelSO;
    [SerializeField]
    private AudioClip audioClip;

    public void PlayAudio()
    {
        audioChannelSO.PlayAudio(audioClip);
    }
}
