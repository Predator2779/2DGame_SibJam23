using UnityEngine;

public class AudioChanger : MonoBehaviour
{
    private AudioClip _currentLocation;

    private void PlayLoop(AudioSource source)
    {
        source.loop = true;
        source.Play();
    }
}