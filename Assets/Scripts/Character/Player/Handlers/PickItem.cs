using System;
using UnityEngine;

namespace Scripts.Character.Player.Handlers
{
    public class PickItem : MonoBehaviour
    {
        [SerializeField] private ItemHandler _itemHandler;
        [SerializeField] private WeaponHandler _weaponHandler;
        [SerializeField] private TransportableItem _selectedItem;

        private void OnValidate() => SetNullableFields();
        private void Start() => SetNullableFields();

        private void SetNullableFields()
        {
            _itemHandler ??= GetComponent<ItemHandler>();
            _weaponHandler ??= GetComponent<WeaponHandler>();
        }

        public void PressedE()
        {
            if (_selectedItem != null)
            {
                if (_selectedItem.TryGetComponent(out Weapon weapon))
                {
                    _weaponHandler.PickUpWeapon(weapon);
                    _selectedItem = null;
                    return;
                }
                
                if (_itemHandler.IsHolded) _itemHandler.PutItem();
                else _itemHandler.PickUpItem(_selectedItem);
                _selectedItem = null;
                
                return;
            }

            Put();
        }

        private void PickUp()
        {
            // _itemHandler.PickUpItem();
            // _weaponHandler.PickUpItem();
            // SetPlayerSide(GetLastPlayerSide());
        }

        private void Put()
        {
            if (_itemHandler.IsHolded)
                _itemHandler.PutItem();
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
}