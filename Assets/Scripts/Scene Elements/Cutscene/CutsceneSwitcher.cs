using System.Collections.Generic;
using Scripts.Core.Global;
using UnityEngine;

public class CutsceneSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] _cutScenes;
    [SerializeField] private GameObject _levelAudio;

    private GameObject[] _slides;
    private bool _isShowed;
    private int _currentCutscene;
    private int _currentSlide;
    private GameStates _gStates;
    private bool _final;

    private void Start()
    {
        InitializeCutscene();
        EventHandler.OnBossKilled.AddListener(ActivateFinalScene);
    }

    private void Update()
    {
        if (!_isShowed) return;

        if (Input.GetKeyUp(KeyCode.Space)) NextScene();
    }

    public void InitializeCutscene()
    {
        _isShowed = true;
        _levelAudio.SetActive(false);
        _slides = GetAllChilds(_cutScenes[_currentCutscene]);
        _cutScenes[_currentCutscene].SetActive(true);
        _slides[_currentSlide].SetActive(true);
    }

    private void NextScene()
    {
        _slides[_currentSlide].SetActive(false);

        if (_slides.Length > _currentSlide + 1)
        {
            _currentSlide++;
            _slides[_currentSlide].SetActive(true);
            return;
        }

        FinilizeCutscene();
    }

    private GameObject[] GetAllChilds(GameObject g)
    {
        List<GameObject> gObjs = new();

        int count = g.transform.childCount;

        for (int i = 0; i < count; i++)
            gObjs.Add(g.transform.GetChild(i).gameObject);

        return gObjs.ToArray();
    }

    private void ActivateFinalScene()
    {
        _levelAudio.SetActive(false);
        _final = true;
        _cutScenes[_cutScenes.Length - 1].SetActive(true);
        _currentSlide = 0;
        _slides = GetAllChilds(_cutScenes[_currentCutscene]);
        _slides[_currentSlide].SetActive(true);
        _isShowed = true;
    }


    private void FinilizeCutscene()
    {
        _slides[_currentSlide].SetActive(false);
        _cutScenes[_currentCutscene].SetActive(false);
        _currentSlide = 0;
        _currentCutscene++;
        _levelAudio.SetActive(true);
        _isShowed = false;

        if (_final) EventHandler.OnLevelDestroy?.Invoke();
    }
}