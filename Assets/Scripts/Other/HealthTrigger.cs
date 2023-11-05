using System.Collections.Generic;
using UnityEngine;

public class HealthTrigger : MonoBehaviour
{
    public List<HealthProcessor> _healthProcessors = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthProcessor healthProcessor)
            && !_healthProcessors.Contains(healthProcessor))
            _healthProcessors.Add(healthProcessor);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthProcessor healthProcessor)
            && _healthProcessors.Contains(healthProcessor))
            _healthProcessors.Remove(healthProcessor);
    }
}