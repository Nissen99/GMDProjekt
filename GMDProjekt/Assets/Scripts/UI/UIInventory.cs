using System.Collections;
using System.Collections.Generic;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    private Inventory _inventory;
    public GameObject BagItemButtonPrefab;
    public GameObject EquippedItemButtonPrefab;
    public Transform BagPanel;
    public Transform EquippedPanel;

    public Sprite RareBackground;
    public Sprite EpicBackground;
    public Sprite LegendaryBackground;

    private List<GameObject> _itemButtons = new List<GameObject>();

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Clear the existing item buttons
        foreach (GameObject button in _itemButtons)
        {
            Destroy(button);
        }

        _itemButtons.Clear();

        // Create new item buttons for the items in the player's bag
        foreach (Item item in _inventory.Bag)
        {
            GameObject button = Instantiate(BagItemButtonPrefab, BagPanel);
            changeButton(button, item);
            button.GetComponent<Button>().onClick.AddListener(() => EquipItem(item));
            _itemButtons.Add(button);
        }

        // Create new item buttons for the player's equipped items
        foreach (Item item in _inventory.EquippedItems)
        {
            GameObject button = Instantiate(EquippedItemButtonPrefab, EquippedPanel);
            changeButton(button, item);
            button.GetComponent<Button>().onClick.AddListener(() => UnequipItem(item));
            _itemButtons.Add(button);
        }
    }

    void changeButton(GameObject button, Item item)
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = item.NameOfItem;
        Image image = button.GetComponent<Image>();
        switch (item.Rarity)
        {
            case Rarity.Rare:
                image.sprite = RareBackground;
                break;
            case Rarity.Epic:
                image.sprite = EpicBackground;
                break;
            case Rarity.Legendary:
                image.sprite = LegendaryBackground;
                break;
        }
    }

    public void EquipItem(Item item)
    {
        _inventory.EquipItem(item);
        UpdateUI();
    }

    public void UnequipItem(Item item)
    {
        _inventory.UnequipItem(item);
        UpdateUI();
    }
}