using Scripts.Other;
using TMPro;
using UnityEngine;

public class Reflections : MonoBehaviour
{
    [SerializeField] private GameObject _dialogWindow;
    [SerializeField] private TMP_Text _tmpText;

    public void Say(bool enabling, string text)
    {
        EnableDialog(enabling);
        SetText(text);
    }

    private void SetText(string text) => _tmpText.text = text;

    private void EnableDialog(bool value) => _dialogWindow.SetActive(value);

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out PointInteres point))
            Say(true, point.textReflection);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PointInteres point))
            Say(false, "");
    }
}