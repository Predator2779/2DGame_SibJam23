using UnityEngine;

namespace Character.Item
{
    public class UsableQuestItem : MonoBehaviour, IUsable
    {
        public string nameItem;
        private bool _canUse;

        private void Start() => EventHandler.OnItemSetUsable.AddListener(SetCanUse);
        
        public void Use() => EventHandler.OnItemUse?.Invoke(nameItem);
        
        public string GetName() => nameItem;
        
        private void SetCanUse(string name) { if (name == nameItem) _canUse = true; }
    }
}