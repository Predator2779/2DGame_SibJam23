using Character.Item;
using UnityEngine;

public class TransportableItem : MonoBehaviour
{
    private IUsable _usableItem;
    private bool _isNotTaken = true;

    private void Start() => _usableItem ??= GetComponent<IUsable>();

    public void PickUp(Transform parent)
    {
        if (_isNotTaken)
        {
            transform.SetParent(parent.transform);
            _isNotTaken = false;
            
            if (_usableItem != null) EventHandler.OnItemPick?.Invoke(_usableItem.GetName());
        }
    }

    public void Put()
    {
        if (!_isNotTaken)
        {
            transform.SetParent(null);
            _isNotTaken = true;
            
            if (_usableItem != null) EventHandler.OnItemPut?.Invoke(_usableItem.GetName());
        }
    }
}