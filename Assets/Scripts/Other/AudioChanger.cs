using UnityEngine;

public class AudioChanger : MonoBehaviour
{
    [Header("Evil Sounds")]
    [SerializeField] private AudioClip _midEvil;
    [SerializeField] private AudioClip _highEvil;

    private AudioClip _currentEvil;

    [Header("Village Sounds")]
    [SerializeField] private AudioClip _villageLowEvil;
    [SerializeField] private AudioClip _villageMidEvil;
    [SerializeField] private AudioClip _villageHighEvil;

    [Header("Location Sounds")]
    [SerializeField] private AudioClip _tavernLocation;
    [SerializeField] private AudioClip _seaLocation;

    private AudioClip _currentLocation;//

    private AudioSource _evilAudio;
    private AudioSource _ambientAudio;

    private void Start()
    {
        _evilAudio = transform.GetChild(0).GetComponent<AudioSource>();
        _ambientAudio = transform.GetChild(1).GetComponent<AudioSource>();

        EventHandler.OnEvilLevelChange.AddListener(ChangeCurrentEvil);
        EventHandler.OnEvilLevelChange.AddListener(ChangeVillageAudio);
    }

    private void ChangeCurrentEvil(int evilLevel)
    {
        // if (evilLevel <= 5)
        // {
        //     _evilAudio.Stop();
        //     _evilAudio.clip = null;
        // }
        // else if (evilLevel <= 8)
        // {
        //     _evilAudio.clip = _midEvil;
        //     _evilAudio.loop = true;
        //     _evilAudio.Play();
        // }
        // else
        // {
        //     _evilAudio.clip = _highEvil;
        //     _evilAudio.loop = true;
        //     _evilAudio.Play();
        // }
        
        if (evilLevel <= 5)
        {
            _evilAudio.Stop();
            _evilAudio.clip = null;
        }
        else if (evilLevel <= 8) _evilAudio.clip = _midEvil;
        else _evilAudio.clip = _highEvil;
        
        PlayLoop(_evilAudio);
    }

    private void ChangeVillageAudio(int evilLevel)
    {
        if (evilLevel <= 2) _ambientAudio.clip = _villageLowEvil;
        else if (evilLevel <= 4) _ambientAudio.clip = _villageMidEvil;
        else _ambientAudio.clip = _villageHighEvil;
        
        PlayLoop(_ambientAudio);
    }

    private void PlayLoop(AudioSource source)
    {
        source.loop = true;
        source.Play();
    }
}