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

    public bool IsHolded { get; private set; }
    public Weapon HoldedWeapon => _holdedWeapon;

    // ReSharper disable Unity.PerformanceAnalysis
    public void PickUpWeapon(Weapon weapon)
    {
        _holdedWeapon = weapon;

        SetIconToUI(_holdedWeapon.gameObject);
        SwitchSpriteItem(_holdedWeapon.gameObject, null);
        SetCharacterWeapon(_holdedWeapon);

        SetSpriteSortOrder();

        IsHolded = true;
    }

    private void SwitchSpriteItem(GameObject item, Sprite sprite)
    {
        _currentWeaponIcon = item.GetComponentInChildren<SpriteRenderer>().sprite;
        item.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }

    private void SetIconToUI(GameObject item)
    {
        if (item == null)
        {
            _weaponUI.sprite = null;
            _weaponUI.gameObject.SetActive(false);
            return;
        }

        var icon = item.GetComponentInChildren<SpriteRenderer>().sprite;
        _weaponUI.sprite = icon;
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

        warrior.weapon = weapon;
    }
}