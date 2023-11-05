using Scripts.Character.Classes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private Person person;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private Image _itemUI;
    [SerializeField] private Image _weaponUI;

    private TransportableItem _selectedItem;
    private TransportableItem _holdedItem;
    private Sprite _currentItemIcon;
    private Sprite __currentWeaponIcon;
    private int _itemSortingOrder;

    public bool IsHolded { get; private set; }
    public TransportableItem HoldedItem => _holdedItem;

    // ReSharper disable Unity.PerformanceAnalysis
    public void PickUpItem()
    {
        if (_selectedItem != null)
        {
            if (_selectedItem.TryGetComponent(out Weapon _)) return;

            _selectedItem.PickUp(transform);
            _holdedItem = _selectedItem;
            _selectedItem = null;

            SetIconToUI(_holdedItem);
            SwitchSpriteItem(_holdedItem, null);
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
            SwitchSpriteItem(_holdedItem, _currentItemIcon);
            SetIconToUI(null);

            _holdedItem.Put();
            _holdedItem = null;
            SetCharacterItem(null);

            IsHolded = false;
        }
    }

    private void SwitchSpriteItem(TransportableItem item, Sprite sprite)
    {
        _currentItemIcon = item.GetComponentInChildren<SpriteRenderer>().sprite;
        item.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }

    private void SetIconToUI(TransportableItem item)
    {
        if (item == null)
        {
            _itemUI.sprite = null;
            _itemUI.gameObject.SetActive(false);
            return;
        }

        var icon = item.GetComponentInChildren<SpriteRenderer>().sprite;
        _itemUI.sprite = icon;
        _itemUI.gameObject.SetActive(true);
    }

    private void SetSpriteSortOrder()
    {
        var tempSprite = GetSpriteRenderer(_holdedItem.transform);

        _itemSortingOrder = tempSprite.sortingOrder;

        tempSprite.sortingOrder = _playerSpriteRenderer.sortingOrder + 1;
    }

    private SpriteRenderer GetSpriteRenderer(Transform t) => t.GetChild(0).GetComponent<SpriteRenderer>();

    private void SetCharacterItem(TransportableItem item)
    {
        if (item == null)
        {
            person.item = null;
            return;
        }

        if (item.TryGetComponent(out UsableItem usable))
            person.item = usable;
    }

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