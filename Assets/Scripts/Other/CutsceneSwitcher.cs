using System.Collections.Generic;
using UnityEngine;

public class CutsceneSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] _cutScenes;
    
    private GameObject[] _slides;
    private bool _isShowed;
    private int _currentCutscene;
    private int _currentSlide;

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

        if (_slides.Length < _currentSlide + 1)
        {
            FinilizeCutscene();
            return;
        }

        _currentSlide++;
        _slides[_currentSlide].SetActive(true);
    }

    private GameObject[] GetAllChilds(GameObject g)
    {
        List<GameObject> gObjs = new();
        
        int count = g.transform.childCount;

        for (int i = 0; i < count; i++)
            gObjs.Add(g.transform.GetChild(i).gameObject);

        return gObjs.ToArray();
    }

    private void FinilizeCutscene()
    {
        _currentCutscene++;
        _slides[_currentSlide].SetActive(false);
        _cutScenes[_currentCutscene].SetActive(false);
        _isShowed = false;
    }
}