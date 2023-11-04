using Scripts.Character.Classes;
using UnityEngine;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private Image _itemIcon; 
    
    private TransportableItem _selectedItem;
    private TransportableItem _holdedItem;
    private int _itemSortingOrder;

    public bool IsHolded { get; private set; }
    public TransportableItem HoldedItem => _holdedItem;

    // ReSharper disable Unity.PerformanceAnalysis
    public void PickUpItem()
    {
        if (_selectedItem != null)
        {
            _selectedItem.PickUp(transform);
            _holdedItem = _selectedItem;
            _selectedItem = null;

            SetIcon(_holdedItem);
            SetCharacterItem(_holdedItem);

            SetSpriteSortOrder();

            IsHolded = true;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void PutItem()
    {
        if (_holdedItem != null)
        {
            GetSpriteRenderer(_holdedItem.transform).sortingOrder = _itemSortingOrder;

            _holdedItem.Put();
            _holdedItem = null;

            SetIcon(null);
            SetCharacterItem(null);

            IsHolded = false;
        }
    }

    private void SetIcon(TransportableItem item)
    {
        if (item == null)
        {
            _itemIcon.sprite = null;
            _itemIcon.gameObject.SetActive(false);
            return;
        }
        
        var icon = item.GetComponentInChildren<SpriteRenderer>().sprite;
        _itemIcon.sprite = icon;
        _itemIcon.gameObject.SetActive(true);
    }
    
    private void SetSpriteSortOrder()
    {
        var tempSprite = GetSpriteRenderer(_holdedItem.transform);

        _itemSortingOrder = tempSprite.sortingOrder;

        tempSprite.sortingOrder = _playerSpriteRenderer.sortingOrder + 1;
    }

    private SpriteRenderer GetSpriteRenderer(Transform t) => t.GetChild(0).GetComponent<SpriteRenderer>();

    private void SetCharacterItem(TransportableItem item) => _character.holdedItem = item;//

    public void SelectItem(TransportableItem item)
    {
        if (IsNewItem(item)) _selectedItem = item;
    }

    public void DeselectItem(TransportableItem item)
    {
        if (!IsNewItem(item)) _selectedItem = null;
    }

    private bool IsNewItem(TransportableItem item) => _selectedItem != item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TransportableItem item))
            _selectedItem = item;
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TransportableItem item) && item == _selectedItem)
            _selectedItem = null;
    }
}