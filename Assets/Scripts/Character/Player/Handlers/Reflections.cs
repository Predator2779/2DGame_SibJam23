using System;
using Scripts.Other;
using TMPro;
using UnityEngine;

public class Reflections : MonoBehaviour
{
    [SerializeField] private GameObject _dialogWindow;
    [SerializeField] private TMP_Text _tmpText;

    private void SetText(string text) => _tmpText.text = text;

    private void EnableDialog() => _dialogWindow.SetActive(true);

    private void DisableDialog() => _dialogWindow.SetActive(false);
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out PointInteres point))
        {
            EnableDialog();
            SetText(point.textReflection);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        SetText("");
        DisableDialog();
    }
}
