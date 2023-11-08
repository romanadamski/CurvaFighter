using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioChannel", menuName = "ScriptableObjects/Audio Channel")]
public class AudioChannelSO : ScriptableObject
{
    public event Action<AudioClip> OnPlayAudio;

    public void PlayAudio(AudioClip audioClip)
    {
        OnPlayAudio?.Invoke(audioClip);
    }
}
