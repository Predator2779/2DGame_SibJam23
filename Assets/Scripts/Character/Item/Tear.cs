using UnityEngine;

namespace Character.Item
{
    public class Tear : UsableItem
    {
        [SerializeField] private int tearPoints;
        public int TearPoints => tearPoints;
        
        public override void PrimaryAction()
        {
            if (!_canUse) return;

            if (_responseItems != null)
                foreach (var responsable in _responseItems)
                    responsable.ResponseAction(gameObject);

            if (_destroyAfterUsing)
                DestroyItem();

            StartCoroutine(CanUse());
        }
    }
}