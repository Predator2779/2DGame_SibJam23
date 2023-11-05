using System.Collections;
using System.Collections.Generic;
using Character.Health;
using UnityEngine;

namespace Character.Item
{
    public class Tear : MonoBehaviour
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
        protected List<TearProcessor> _responseItems = new();

        private void Start()
        {
            _canUse = true;
        }

        public void PrimaryAction()
        {
            UseResponsable();
        }

        [SerializeField] private int tearPoints;
        public int TearPoints => tearPoints;

        protected void UseResponsable()
        {
            if (!_canUse) return;

            if (_responseItems != null)
                foreach (var responsable in _responseItems)
                    if (responsable.TryGetComponent(out TearProcessor tearProcessor))
                        tearProcessor.ResponseAction(gameObject);

            StartCoroutine(CanUse());

            if (_destroyAfterUsing)
                DestroyItem();
        }

        protected void DestroyItem()
        {
            transform.GetComponentInChildren<SpriteRenderer>().enabled = false;
            EventHandler.OnItemDestroy?.Invoke();
        }

        public void PassiveAction()
        {
            if (_passiveExecution) PrimaryAction();
        }

        protected IEnumerator CanUse()
        {
            _canUse = false;
            yield return new WaitForSeconds(_delayUsing);
            _canUse = true;
        }

        private void AddToList(Collider2D collision)
        {
            if (!collision.TryGetComponent(out TearProcessor usable)) return;
            if (!_responseItems.Contains(usable))
                _responseItems.Add(usable);
        }

        private void RemoveFromList(Collider2D collision)
        {
            if (!collision.TryGetComponent(out TearProcessor usable)) return;
            if (_responseItems.Contains(usable))
                _responseItems.Remove(usable);
        }

        protected void OnTriggerEnter2D(Collider2D collision) => AddToList(collision);

        protected void OnTriggerStay2D(Collider2D collision)
        {
            PassiveAction();
        }

        protected void OnTriggerExit2D(Collider2D collision) => RemoveFromList(collision);
    }
}