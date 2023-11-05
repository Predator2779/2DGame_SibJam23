using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : MonoBehaviour
{
    public string nameItem;

    [Tooltip("Одноразовые предметы исчезают после использования")] [SerializeField]
    protected bool _destroyAfterUsing;
    [Tooltip("Выполняется непрерывно")] [SerializeField]
    protected bool _continuousExecution;
    [Tooltip("Выполняется при попадании в триггер")] [SerializeField]
    protected bool _passiveExecution;
    [Tooltip("Задержка использования")] [SerializeField]
    protected float _delayUsing = 0.1f;
    [Tooltip("Удаление компонента у предмета, после использования")] [SerializeField]
    protected bool _disableComponent;

    protected bool _canUse = true;
    protected List<HealthProcessor> _responseItems = new();

    private void Start() => _canUse = true;

    public void PrimaryAction() => UseResponsable();

    private void UseResponsable()
    {
        if (!_canUse || !enabled) return;

        if (_responseItems != null)
            foreach (var responsable in _responseItems)
                responsable.ResponseAction(gameObject);

        StartCoroutine(CanUse());

        if (_disableComponent)
            enabled = false;

        if (_destroyAfterUsing)
            Destroy(gameObject);
    }

    private void PassiveAction()
    {
        if (_passiveExecution) PrimaryAction();
    }

    private IEnumerator CanUse()
    {
        _canUse = false;
        yield return new WaitForSeconds(_delayUsing);
        _canUse = true;
    }

    private void AddToList(Collider2D collision)
    {
        if (!collision.TryGetComponent(out HealthProcessor usable)) return;
        if (!_responseItems.Contains(usable))
            _responseItems.Add(usable);
    }

    private void RemoveFromList(Collider2D collision)
    {
        if (!collision.TryGetComponent(out HealthProcessor usable)) return;
        if (_responseItems.Contains(usable))
            _responseItems.Remove(usable);
    }

    protected void OnTriggerEnter2D(Collider2D collision) => AddToList(collision);
    protected void OnTriggerStay2D(Collider2D collision) => PassiveAction();
    protected void OnTriggerExit2D(Collider2D collision) => RemoveFromList(collision);
}