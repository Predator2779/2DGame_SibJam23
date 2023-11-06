using System.Collections.Generic;
using UnityEngine;

public class CutsceneSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] _cutScenes;

    private GameObject[] _slides;
    private bool _isShowed;
    private int _currentCutscene;
    private int _currentSlide;

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
        _slides[_cutScenes.Length - 1].SetActive(true);
        _slides[_currentSlide].SetActive(true);
    }

    private void FinilizeCutscene()
    {
        _slides[_currentSlide].SetActive(false);
        _cutScenes[_currentCutscene].gameObject.SetActive(false);
        _currentSlide = 0;
        _currentCutscene++;
        _isShowed = false;
    }
}