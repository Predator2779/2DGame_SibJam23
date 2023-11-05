using UnityEngine;

namespace Character.Item
{
    public class Tear : MonoBehaviour
    {
        [SerializeField] private bool _destroyAfterUsing;
        [SerializeField] private int tearPoints;
        public int TearPoints => tearPoints;

        private void PrimaryAction(TearProcessor tearProcessor)
        {
            tearProcessor.ResponseAction(this);

            if (_destroyAfterUsing)
                Destroy(gameObject);
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out TearProcessor tearProcessor))
                PrimaryAction(tearProcessor);
        }
    }
}