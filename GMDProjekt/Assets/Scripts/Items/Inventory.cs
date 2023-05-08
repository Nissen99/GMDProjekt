using System;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Random = System.Random;

namespace Items
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> EquippedItems { get; set; }
        public List<Item> Bag { get; set; }
        public int BagSize;
        public int MaxNoEquiped;

        public GearChangedEvent onMaxHealthChange;

        private void Awake()
        {
            onMaxHealthChange = new GearChangedEvent();
            EquippedItems = new List<Item>();
            Bag = new List<Item>();
            spawnInItemsToTestWith();
        }

        public void EquipItem(Item item)
        {
            if (!(EquippedItems.Count < MaxNoEquiped))
            {
                return;
            }
            EquippedItems.Add(item);
            Bag.Remove(item);
            onMaxHealthChange.Invoke();
        }

        public void UnequipItem(Item item)
        {
            EquippedItems.Remove(item);
            Bag.Add(item);
            onMaxHealthChange.Invoke();
        }

        public void AddItemToBag(Item item)
        {
            if (!(Bag.Count < BagSize))
            {
                return;
            }
            Bag.Add(item);
        }

        public void RemoveItemFromBag(Item item)
        {
            Bag.Remove(item);
        }

        public int GetAmountOfMainStat()
        {
            var count = 0;
            foreach (var equippedItem in EquippedItems)
            {
                count += equippedItem.MainStat;
            }
            return count;
        }
        public int GetAmountOfVitality()
        {
            var count = 0;
            foreach (var equippedItem in EquippedItems)
            {
                count += equippedItem.Vitality;
            }
            return count;
        }

        public bool HasItemWithNameEquipped(string nameOfItem)
        {
            foreach (var equippedItem in EquippedItems)
            {
                if (equippedItem.NameOfItem == nameOfItem)
                {
                    return true;
                }
            }

            return false;
        }

        
        //TODO: Remove at some point, this method is just here as items do not drop at this point, remove when normal items impelented
        private void spawnInItemsToTestWith()
        {
            //RateAndEpicItems;
            Random random = new Random();
            string[] itemNames = { "Sword", "Shield", "Helmet", "Armor", "Boots", "Gloves", "Ring", "Amulet", "Belt", "Potion" };

            for (int i = 0; i < 20; i++)
            {
                string name = itemNames[random.Next(itemNames.Length)];
                int mainStat = random.Next(1, 11);
                int vitality = random.Next(1, 5);
                Rarity rarity = (Rarity)random.Next(0, 2);
                Item item = new Item
                {
                    NameOfItem = name,
                    MainStat = mainStat,
                    Vitality = vitality,
                    Rarity = rarity
                };
                Bag.Add(item);
            }
            
            //2 legendary inspired from d3 
            Bag.Add(new Item
            {
                NameOfItem = Legendary_Item_Names.HOLY_POINT_SHOT,
                MainStat = 15,
                Vitality = 10,
                Rarity = Rarity.Legendary,
                LegendaryEffect = "Impale throws two additional knives"
            });
            Bag.Add(new Item
            {
                NameOfItem = Legendary_Item_Names.KARLEIS_POINT,
                MainStat = 20,
                Vitality = 11,
                Rarity = Rarity.Legendary,
                LegendaryEffect = "Impale deals 3.75 times increased damage"
            });
        }
    }
}