using System;
using Scripts.Character.Classes;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Warrior warrior;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private Image _weaponUI;

    private Weapon _holdedWeapon;
    private Sprite _currentWeaponIcon;
    private int _itemSortingOrder;

    private void Start()
    {
        SetIconToUI(warrior.currentWeapon.gameObject);
    }

    public void PickUpWeapon(Weapon weapon) => SetWeapon(weapon);
    public void ChangeWeapon() => SetWeapon(warrior.ChangeWeapon());

    private void SetWeapon(Weapon weapon)
    {
        warrior.AddWeapon(weapon);
        _holdedWeapon = weapon;

        SetIconToUI(_holdedWeapon.gameObject);
        SetCharacterWeapon(_holdedWeapon);
        SetSpriteSortOrder();
    }

    private void SetIconToUI(GameObject item)
    {
        _weaponUI.sprite = item.GetComponentInChildren<SpriteRenderer>().sprite;
        _weaponUI.gameObject.SetActive(true);
    }
    
    private void SetSpriteSortOrder()
    {
        var tempSprite = GetSpriteRenderer(_holdedWeapon.transform);
        _itemSortingOrder = tempSprite.sortingOrder;
        tempSprite.sortingOrder = _playerSpriteRenderer.sortingOrder + 1;
    }

    private SpriteRenderer GetSpriteRenderer(Transform t) => t.GetChild(0).GetComponent<SpriteRenderer>();

    private void SetCharacterWeapon(Weapon weapon)
    {
        if (weapon == null)
        {
            warrior.item = null;
            return;
        }

        warrior.currentWeapon = weapon;
    }
}