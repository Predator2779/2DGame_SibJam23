using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Trigger : MonoBehaviour
{
    public TriggerEnterCallback _triggerEnterCallback;
    public TriggerExitCallback _triggerExitCallback;

    private void OnTriggerEnter2D(Collider2D col)
    {
        _triggerEnterCallback?.Invoke(transform, col);
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        _triggerExitCallback?.Invoke(transform, col);
    }

    public delegate void TriggerEnterCallback(Transform trigger, Collider2D collider);
    public delegate void TriggerExitCallback(Transform trigger, Collider2D collider);
}