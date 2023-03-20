using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameSystemsCookbook
{
    /// <summary>
	/// Class for controlling the AudioModifiers in the demo scene.
	/// </summary>
    public class AudioSourceController : MonoBehaviour
    {
        // Cache the AudioModifier components in the scene. Track the currently active index.
        private AudioModifier[] m_AudioSources;
        private int m_CurrentIndex;

        private void Awake()
        {
            m_AudioSources = GetComponentsInChildren<AudioModifier>();
        }

        // Locate the nearest AudioModifer and play the clip.
        public void PlayClosestSource(Vector2 position)
        {
            AudioModifier closestAudioModifier = null; 
            float closestDistance = float.MaxValue;

            foreach (AudioModifier audioModifier in m_AudioSources)
            {
                float distance = Vector2.Distance(position, audioModifier.transform.position);

                if (distance < closestDistance)
                {
                    closestAudioModifier = audioModifier;
                    closestDistance = distance;
                }
            }

            if (closestAudioModifier != null)
            {
                closestAudioModifier.Play();
            }
        }

        // Selects the next available AudioModifier GameObject.
        public void SelectNextAudioSource()
        {
            GameObject selectedAudioObject = m_AudioSources[m_CurrentIndex].gameObject;

            Selection.activeGameObject = selectedAudioObject;
            m_CurrentIndex++;

            if (m_CurrentIndex >= m_AudioSources.Length)
                m_CurrentIndex = 0;
        }
    }
}
