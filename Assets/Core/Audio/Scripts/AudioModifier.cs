using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Alters the playback of an AudioSource using a clip from AudioDelegateSO. The rules for
/// modifying the sound come from the SimpleAudioDelegate ScriptableObject.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioModifier : MonoBehaviour
{
    // This ScriptableObject actually holds the methods for playing back sound. In this way, we
    // implement the strategy pattern where the delegate objects can be swapped.
    [SerializeField] private AudioDelegateSO m_AudioDelegate;

    private AudioSource m_AudioSource;

    // Gets a reference to the attached AudioSource component
    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Plays the AudioSource
    public void Play()
    {
        if (m_AudioDelegate == null)
            return;

        m_AudioDelegate.Play(m_AudioSource);
    }
}
