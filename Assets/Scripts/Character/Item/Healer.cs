using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private bool _destroyAfterUsing;
    [SerializeField] private int healPoints;
    public int HealPoints => healPoints;

    private void PrimaryAction(HealthProcessor tearProcessor)
    {
        tearProcessor.ResponseAction(gameObject);

        if (_destroyAfterUsing)
            Destroy(gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthProcessor tearProcessor))
            PrimaryAction(tearProcessor);
    }
}