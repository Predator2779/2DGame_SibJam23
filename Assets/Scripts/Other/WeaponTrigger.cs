using System.Collections.Generic;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    public List<HealthProcessor> _healthProcessors = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthProcessor healthProcessor)
            && !_healthProcessors.Contains(healthProcessor))
        {
            Debug.Log("Add " + other.name + " to " + this);
            _healthProcessors.Add(healthProcessor);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthProcessor healthProcessor)
            && _healthProcessors.Contains(healthProcessor))
        {
            Debug.Log("Remove " + other.name + " from " + this);
            _healthProcessors.Remove(healthProcessor);
        }
    }
}