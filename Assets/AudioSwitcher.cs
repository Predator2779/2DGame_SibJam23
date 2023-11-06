using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitcher : MonoBehaviour
{
    public GameObject _audio;
    
    void Start()
    {
        EventHandler.OnPlayerDeath.AddListener(DisableMusic);
    }

    private void DisableMusic()
    {
        _audio.SetActive(false);
    }
}
