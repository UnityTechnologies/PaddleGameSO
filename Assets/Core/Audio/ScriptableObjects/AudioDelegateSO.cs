using UnityEngine;
using System;

/// <summary>
/// This struct defines a minimum and maximum range of float values.
/// </summary>
[Serializable]
public struct RangedFloat
{
	public float minValue;
	public float maxValue;
}

/// <summary>
/// An example of using a ScriptableObject-based delegate. This
/// encapsulates some methods to play back a sound.
/// </summary>
public abstract class AudioDelegateSO : ScriptableObject
{
	public abstract void Play(AudioSource source);
}
