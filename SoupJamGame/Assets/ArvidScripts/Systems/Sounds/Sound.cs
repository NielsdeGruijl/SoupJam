using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    [Header("Settings")]
    [Range(0f, 1f)] public float volume = 0.3f;
    [Range(0.1f, 3f)] public float pitch = 1f;

    [HideInInspector] public AudioSource source;
}
