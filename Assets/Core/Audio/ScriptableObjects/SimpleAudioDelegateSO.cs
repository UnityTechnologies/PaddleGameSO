using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// An AudioDelegate that plays back a random clip with variations in pitch and volume.
/// Use a MonoBehaviour (see the AudioModifier example script) to attach to a GameObject.
/// </summary>
[CreateAssetMenu(fileName ="SimpleAudioDelegate",menuName ="PaddleBall/Simple Audio Delegate")]
public class SimpleAudioDelegateSO : AudioDelegateSO
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private RangedFloat volume;
    [SerializeField] private RangedFloat pitch;

    // If we have a valid clip, select a random clip, volume, and pitch. Then, play the sound.
    public override void Play(AudioSource source)
    {
        if (clips.Length == 0 || source == null)
            return;

        source.clip = clips[Random.Range(0, clips.Length)];
        source.volume = Random.Range(volume.minValue, volume.maxValue);
        source.pitch = Random.Range(pitch.minValue, pitch.maxValue);

        source.Play();
    }
}
