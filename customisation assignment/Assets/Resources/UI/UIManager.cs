using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{


    [SerializeField] private Collider playerCollider; 
    [SerializeField] private GameObject dropSpawner;
    [SerializeField] private GameObject InvetoryPanel;
    [SerializeField] private GameObject StatPanel;
    [SerializeField] private GameObject Crosshair;
    [SerializeField] private bool firstPerson;
    public static bool refreshItems;
    public static int refreshSlot;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject Map;
    [SerializeField] private Text helpText;
    [SerializeField] public bool helpOn;
    public static bool dead;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject AxeObject;
    [SerializeField] private GameObject HeadObject;
    public bool mapUp;

    [SerializeField] private CanvasGroup pickup;

    [SerializeField] private bool inventoryUp;
    [SerializeField] private bool statsUp;
    [SerializeField] private Canvas canvas;
    [SerializeField] public static List<Items> inventoryItems = new List<Items>();
    [SerializeField] public List<Sprite> itemSprites = new List<Sprite>();

    [SerializeField] public int itemIndex;
    [SerializeField] public int inventoryLength;
    [SerializeField] public Items collidedItem;
    [SerializeField] public GameObject[] inventoryslots;
    [SerializeField] public Image[] itemPicture;
    [SerializeField] public static Items[] InGameItems;
    [SerializeField] public GameObject[] ItemPrefabs;
    [SerializeField] public Text[] slottedAmount;
    [SerializeField] public Text[] slottedItemWorth;
    [SerializeField] public Text[] slottedItemName;
    [SerializeField] public Text[] itemTypeText;
    [SerializeField] public Image[] EquippedItems;
    [SerializeField] public Text[] EquippedItemText;
    [SerializeField] private int[] equippedItemint;
    [SerializeField] private bool[] equippedItembool;
    [SerializeField] private bool type;
    [SerializeField] private Text sortText;
    [SerializeField] public static int currency;
    [SerializeField] private Text currencyText;
    [SerializeField] private GameObject neckLight;

    #region new items
    [SerializeField] public Items HealthPotion;
    [SerializeField] public Items Apple;
    [SerializeField] public Items Armour;
    [SerializeField] public Items Axe;
    [SerializeField] public Items Belt;
    [SerializeField] public Items Bow;
    [SerializeField] public Items Book;
    [SerializeField] public Items Boots;
    [SerializeField] public Items Bracers;
    [SerializeField] public Items Cloak;
    [SerializeField] public Items Gem;
    [SerializeField] public Items Gloves;
    [SerializeField] public Items Helmet;
    [SerializeField] public Items Ingot;
    [SerializeField] public Items Meat;
    [SerializeField] public Items ManaPotion;
    [SerializeField] public Items Necklace;
    [SerializeField] public Items Pants;
    [SerializeField] public Items Rings;
    [SerializeField] public Items Scroll;
    [SerializeField] public Items Shield;
    [SerializeField] public Items Shoulders;
    [SerializeField] public Items Sword;
    #endregion


    //list comparers 
    public class SortType : IComparer<Items>
    {
        int IComparer<Items>.Compare(Items a, Items b)
        {
            string com1 = a.itemType.ToString();
            string com2 = b.itemType.ToString();
            return com1.CompareTo(com2);
        }
    }

    public class SortName : IComparer<Items>
    {
        int IComparer<Items>.Compare(Items a, Items b)
        {
            string com1 = a.name;
            string com2 = b.name;
            return com1.CompareTo(com2);
        }
    }


    void Start()
    {
        equippedItembool = new bool[14];

        //should read pain in the neck, semi-sure the load function is redundant. oh well
        // also load is not allowed outside of a method so its here instead of the ugly block above
        #region item initialisation
        HealthPotion = new Items("Health Potion", 10, ItemType.Consumable, Resources.Load("UI/Icons/hp") as Sprite, 0, 0, "heals 50 damage over 5 seconds");
        Apple = new Items("Apple", 1, ItemType.Consumable, Resources.Load("UI/Icons/apple") as Sprite, 1, 0, "Yummy!, gives you 5 hp");
        Armour = new Items("Armour", 60, ItemType.ChestArmour, Resources.Load("UI/Icons/armour") as Sprite, 2, 0, "Leather Armour: 5 defense");
        Axe = new Items("Axe", 20, ItemType.PrimaryWeapon, Resources.Load("UI/Icons/axe 1") as Sprite, 3, 0, "Axe: 6 Attack");
        Bow = new Items("Bow", 30, ItemType.RangedWeapon, Resources.Load("UI/Icons/b_t_01 1") as Sprite, 4, 0, "Bow: 3 Attack");
        Book = new Items("Book", 10, ItemType.Treasure, Resources.Load("UI/Icons/book 1") as Sprite, 5, 0, "A valuble book");
        Boots = new Items("Boots", 10, ItemType.FootArmour, Resources.Load("UI/Icons/boots 1") as Sprite, 6, 0, "Leather Boots: 2 defense");
        Bracers = new Items("Bracers", 12, ItemType.WristArmour, Resources.Load("UI/Icons/bracers") as Sprite, 7, 0, "Leather Bracers: 1 attack, 1 defense");
        Cloak = new Items("Cloak", 15, ItemType.Cloak, Resources.Load("UI/Icons/cloaks") as Sprite, 8, 0, "A warm cloak: 1 defense");
        Gem = new Items("Gem", 100, ItemType.Treasure, Resources.Load("UI/Icons/gem") as Sprite, 9, 0, "Ohh... Shiney");
        Gloves = new Items("Gloves", 15, ItemType.HandArmour, Resources.Load("UI/Icons/gloves") as Sprite, 10, 0, "Leather Gloves:  2 defense, 1 attack");
        Helmet = new Items("Helmet", 30, ItemType.HeadArmour, Resources.Load("UI/Icons/helmets") as Sprite, 11, 0, "A simple Helmet: 3 defense");
        Ingot = new Items("Ingot", 5, ItemType.Material, Resources.Load("UI/Icons/ingots") as Sprite, 12, 0, "a crafting material");
        Meat = new Items("Meat", 8, ItemType.Consumable, Resources.Load("UI/Icons/Meat") as Sprite, 13, 0, "A hearty meal! heals 10 hp");
        ManaPotion = new Items("ManaPotion", 10, ItemType.Consumable, Resources.Load("UI/Icons/mp 1") as Sprite, 14, 0,"Restores 20 mana");
        Necklace = new Items("Necklace", 80, ItemType.NeckSlot, Resources.Load("UI/Icons/necklace 1") as Sprite, 15, 0, "Necklace of charm: +1 charisma");
        Pants = new Items("Pants", 60, ItemType.LegArmour, Resources.Load("UI/Icons/pants 1") as Sprite, 16, 0, "Leather Pants: 5 defense");
        Rings = new Items("Rings", 90, ItemType.RingSlot, Resources.Load("UI/Icons/rings 1") as Sprite, 17, 0, "Ring of Strength: +1 strength");
        Scroll = new Items("Scroll", 110, ItemType.Treasure, Resources.Load("UI/Icons/scroll 1") as Sprite, 18, 0, "Secrets of the ages, on sale now...");
        Shield = new Items("Shield", 30, ItemType.Shield, Resources.Load("UI/Icons/shield") as Sprite, 19, 0, "Wooden Shield: 4 defense");
        Shoulders = new Items("Shoulders", 20, ItemType.Shoulder, Resources.Load("UI/Icons/shoulders") as Sprite, 20, 0,"Exessive Pouldrons of XenoSlaying: + 5 strength, +5 constitution, 7 Attack");
        Sword = new Items("Sword", 30, ItemType.PrimaryWeapon, Resources.Load("UI/Icons/sword") as Sprite, 21, 0, "Sword: 6 Attack");
        Belt = new Items("Belt", 30, ItemType.Belt, Resources.Load("UI/Icons/sword") as Sprite, 21, 0, "Leather Belt: 1 defense, +1 Strength ");

        #endregion
        inventoryItems.Capacity = 7;
        InGameItems = new Items[] { HealthPotion, Apple, Armour, Axe, Bow, Book, Boots, Bracers, Cloak, Gem, Gloves, Helmet, Ingot, Meat, ManaPotion, Necklace, Pants, Rings, Scroll, Shield, Shoulders, Sword, Belt };

        //this could be an array of UI bools, it isnt.
        inventoryUp = false;
        statsUp = false;
        firstPerson = false;
        helpOn = true;
        mapUp = false;

        type = false;

        for (int i = 0; i < equippedItembool.Length; i++)
        {
            equippedItembool[i] = false;
        }
        dead = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            CloseInventory();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CloseStats();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            FpActive();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            MapUp();
        }

        if (refreshItems)
        {
            RefreshOnPickup();
            RefreshDisplay();
        }

        if (dead)
        {
            deathPanel.transform.position = canvas.pixelRect.center;
            mapUp = false;
            inventoryUp = false;
            statsUp = false;
        }
        else
        {
            deathPanel.transform.position  = new Vector3(0, canvas.pixelRect.yMin - canvas.pixelRect.yMax);
        }

    }

    void FixedUpdate()
    {
        //the higher the magic number in the lerp, the faster it go
        if (inventoryUp)
        {
            InvetoryPanel.transform.position = new Vector3(Mathf.Lerp(InvetoryPanel.transform.position.x, canvas.pixelRect.center.x, 4 * Time.deltaTime), canvas.pixelRect.center.y, 0);
        }
        else
        {
            InvetoryPanel.transform.position = new Vector3(Mathf.Lerp(InvetoryPanel.transform.position.x, canvas.pixelRect.xMax + canvas.pixelRect.xMax / 2, 4 * Time.deltaTime), canvas.pixelRect.center.y, 0);
        }

        if (statsUp)
        {
            StatPanel.transform.position = new Vector3(Mathf.Lerp(StatPanel.transform.position.x, canvas.pixelRect.center.x, 4 * Time.deltaTime), canvas.pixelRect.center.y, 0);
        }
        else
        {
            StatPanel.transform.position = new Vector3(Mathf.Lerp(StatPanel.transform.position.x, canvas.pixelRect.xMin - canvas.pixelRect.xMax / 2, 4 * Time.deltaTime), canvas.pixelRect.center.y, 0);
        }

        if (helpOn)
        {
            helpPanel.transform.position = new Vector3(canvas.pixelRect.center.x, Mathf.Lerp(helpPanel.transform.position.y, canvas.pixelRect.center.y, 4 * Time.deltaTime), 0);
        }
        else
        {
            helpPanel.transform.position = new Vector3(canvas.pixelRect.center.x, Mathf.Lerp(helpPanel.transform.position.y, canvas.pixelRect.yMin - canvas.pixelRect.yMax /5, 4 * Time.deltaTime), 0);
        }

        if (mapUp)
        {
            Map.transform.position = new Vector3(canvas.pixelRect.center.x, Mathf.Lerp(Map.transform.position.y, canvas.pixelRect.center.y, 4 * Time.deltaTime), 0);
        }
        else
        {
            Map.transform.position = new Vector3(canvas.pixelRect.center.x, Mathf.Lerp(Map.transform.position.y, canvas.pixelRect.yMin - canvas.pixelRect.yMax / 2, 4 * Time.deltaTime), 0);
        }


    }

    public void MapUp()
    {
        if (mapUp)
        {
            mapUp = false;
        }
        else
        {
            mapUp = true;
        }
    }

    public void FpActive()
    {
        if (firstPerson)
        {
            firstPerson = false;
            Crosshair.SetActive(false);
        }
        else
        {
            Crosshair.SetActive(true);
            firstPerson = true;
        }
    }

    public void HelpPanel()
    {
        if (!helpOn)
        {
            helpOn = true;
            helpText.text = "X";
        }

        else
        {
            helpOn = false;
            helpText.text = "^";
        }

    }

    //tells the inventory where to try and go, out or in.
    public void CloseInventory()
    {
        if (!inventoryUp)
            inventoryUp = true;
        else
            inventoryUp = false;
    }

    public void CloseStats()
    {
        if (!statsUp)
            statsUp = true;
        else
            statsUp = false;
    }

    //same as remove item but with an instatiate part, could probably be condensed but i lack motivation and time
    public void DropItem(int droppedItem)
    {
        Instantiate(ItemPrefabs[inventoryItems[droppedItem].itemNumber], dropSpawner.transform.position, Quaternion.identity);

        InGameItems[inventoryItems[droppedItem].itemNumber].itemCount--;
        RefreshDisplay();
    }

    //remove item
    public void DestroyItem(int itemNumber)
    {
        InGameItems[inventoryItems[itemNumber].itemNumber].itemCount--;
        RefreshDisplay();
    }

    //update the item list
    public void RefreshDisplay()
    {
        foreach (Items stowedItem in inventoryItems.ToArray())
        {
            if(stowedItem.itemCount <= 0)
            {
                inventoryItems.Remove(stowedItem);
                inventoryslots[inventoryItems.Count].SetActive(false);
            }
        }

        if (inventoryItems.Count == 0)
        {
            inventoryslots[0].SetActive(false);
        }

        //make sure the items display correctly
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            inventoryslots[i].SetActive(true);
            itemPicture[i].GetComponent<Image>().sprite = itemSprites[inventoryItems[i].itemNumber];
            itemTypeText[i].text = inventoryItems[i].itemType.ToString();
            slottedAmount[i].text = inventoryItems[i].itemCount.ToString();
            slottedItemWorth[i].text = inventoryItems[i].value.ToString() + " gold";
            slottedItemName[i].text = inventoryItems[i].name;
        }
        refreshItems = false;
        currencyText.text = currency.ToString();

        //turn on your cute necklace glow and visually equip items
        if(equippedItembool[10])
        {
            neckLight.SetActive(true);
        }
        else
        {
            neckLight.SetActive(false);
        }


        if (equippedItembool[0])
        {
            HeadObject.SetActive(true);
        }
        else
        {
            HeadObject.SetActive(false);
        }
        if (equippedItembool[7])
        {
            AxeObject.SetActive(true);
        }
        else
        {
            AxeObject.SetActive(false);
        }

    }

    public void UseItem(int usedItem)
    {
        //this part does not work for some reason and still allows you to use items
        if (inventoryItems[usedItem].itemType == ItemType.Treasure)
            return;

        if (inventoryItems[usedItem].itemType == ItemType.Consumable)
        {
            Function(inventoryItems[usedItem].itemNumber);
        }
        else if (inventoryItems[usedItem].itemType != ItemType.Treasure)
        {
            switch (inventoryItems[usedItem].itemType)
            {
                case ItemType.HeadArmour:
                    Equip(0, 11);
                    break;
                case ItemType.ChestArmour:
                    Equip(1, 2);
                    break;
                case ItemType.LegArmour:
                    Equip(2, 16);
                    break;
                case ItemType.Cloak:
                    Equip(3, 8);
                    break;
                case ItemType.WristArmour:
                    Equip(5, 7);
                    break;
                case ItemType.Shoulder:
                    Equip(4, 20);
                    break;
                case ItemType.PrimaryWeapon:
                    if(inventoryItems[usedItem].itemNumber == 3)
                        Equip(7, 3);
                    else
                        Equip(7, 21);
                    break;
                case ItemType.Shield:
                    Equip(8, 19);
                    break;
                case ItemType.RangedWeapon:
                    Equip(9, 4);
                    break;
                case ItemType.FootArmour:
                    Equip(13, 6);
                    break;
                case ItemType.HandArmour:
                    Equip(6, 10);
                    break;
                case ItemType.NeckSlot:
                    Equip(10, 15);
                    break;
                case ItemType.Belt:
                    Equip(11, 22);
                    break;
                case ItemType.RingSlot:
                    Equip(12, 17);
                    break;
            }

            InGameItems[inventoryItems[usedItem].itemNumber].itemCount--;
        }
    }

    public void Equip(int slotEquip, int descriptionInt)
    {
        //if there is an item in the slot it will be added to the inventory
        Dequip(slotEquip, descriptionInt);

        //activate item sprite
        Color slotColor = EquippedItems[slotEquip].GetComponent<Image>().color;
        slotColor = Color.white;
  
        EquippedItemText[slotEquip].text = InGameItems[descriptionInt].description;
        equippedItemint[slotEquip] = descriptionInt;
        EquippedItems[slotEquip].GetComponent<Image>().sprite = itemSprites[descriptionInt];
        equippedItembool[slotEquip] = true;
    }

    //a button friendly dequip method
    public void DequipButton(int slotDequip)
    {
        if (equippedItembool[slotDequip])
        {
            Dequip(slotDequip, equippedItemint[slotDequip]);
            equippedItembool[slotDequip] = false;
        }
    }
    
    public void Dequip(int slotDequip, int itemInt)
    {
        if (equippedItembool[slotDequip])
        {
            // add to inventory
            inventoryItems.Add(InGameItems[InGameItems[equippedItemint[slotDequip]].itemNumber]);
            inventoryslots[inventoryItems.Count-1].SetActive(true);

            InGameItems[InGameItems[equippedItemint[slotDequip]].itemNumber].itemCount = 1;

            //deactivate item sprite
            Color32 slotColor = EquippedItems[slotDequip].GetComponent<Image>().color;
            slotColor = new Color32(80, 80, 80, 255);
            EquippedItemText[slotDequip].text = "None";
            RefreshDisplay();
        }
    }

    public void Function(int itemNumber)
    {
        switch (itemNumber)
        {
            case 0:
                player.AssignableStatManager.regenerating = true;
                break;
            case 1:
                if(player.AssignableStatManager.regenStats[0][1] <= player.AssignableStatManager.regenStats[0][0] - 5)
                {
                    player.AssignableStatManager.regenStats[0][1] = player.AssignableStatManager.regenStats[0][1] + 5;
                }
                player.AssignableStatManager.healing = true;
                break;
            case 12:
                if (player.AssignableStatManager.regenStats[0][1] <= player.AssignableStatManager.regenStats[0][0] - 20)
                {
                    player.AssignableStatManager.regenStats[0][1] = player.AssignableStatManager.regenStats[0][1] + 20;
                }
                player.AssignableStatManager.healing = true;
                break;
            case 14:
                player.AssignableStatManager.regenStats[2][1] = player.AssignableStatManager.regenStats[2][1] + 20;
                if(player.AssignableStatManager.regenStats[2][1] > player.AssignableStatManager.regenStats[2][0])
                {
                    player.AssignableStatManager.regenStats[2][1] = player.AssignableStatManager.regenStats[2][0];
                }
                break;
            default:
                break;

        }
    }


    //sorts using the constructor.
    public void SortItems()
    {
        if (type && inventoryItems.Count > 0)
        {
            foreach (Items playerItems in inventoryItems.ToArray())
            {
                inventoryItems.Sort(new SortType());
            }

            sortText.text = "Sort:Type";

        }
        
        if(!type && inventoryItems.Count > 0)
        {
            foreach (Items playerItems in inventoryItems.ToArray())
            {
                inventoryItems.Sort(new SortName());
            }

            sortText.text = "Sort:Name";
        }

        if (type)
            type = false;
        else
            type = true;


        RefreshDisplay();
    }

    public void RefreshOnPickup()
    {
        Audio.boop = true;
        if(inventoryItems.Count < inventoryslots.Length)
        {
            if (!inventoryItems.Contains(InGameItems[refreshSlot]) || (inventoryItems.Contains(InGameItems[refreshSlot]) && InGameItems[refreshSlot].itemType != ItemType.Consumable))
            {
                inventoryItems.Add(InGameItems[refreshSlot]);
                InGameItems[refreshSlot].itemCount++;
            }
            else
            {
                InGameItems[refreshSlot].itemCount++;
            }

            StartCoroutine(PanelFlash(pickup));
        }
        else
        {
            Instantiate(ItemPrefabs[refreshSlot], dropSpawner.transform.position, Quaternion.identity);
        }

        RefreshDisplay();

    }

    public static void AddItem(int itemNumber)
    {
        refreshSlot = itemNumber;
        refreshItems = true;
    }

    public IEnumerator PanelFlash(CanvasGroup flashGroup)
    {
        flashGroup.alpha = 1;
        yield return new WaitForSeconds(0.06f);

        flashGroup.alpha = 0;
        yield return null;
    }

}



//allows some space saving higher up as i can just call it on one line.
public class Items
{
    public string name;
    public int value;
    public ItemType itemType;
    public Sprite itemImage;
    public int itemNumber;
    public int itemCount;
    public string description;

    public Items(string _name, int _value, ItemType _itemType, Sprite _itemImage, int _itemNumber, int _itemCount, string _description)
    {
        name = _name;
        value = _value;
        itemType = _itemType;
        itemImage = _itemImage;
        itemNumber = _itemNumber;
        itemCount = _itemCount;
        description = _description;
    }
}

public enum ItemType { Consumable, ChestArmour, FootArmour, HeadArmour, Shield, HandArmour , Shoulder, Cloak, WristArmour, LegArmour, RingSlot, NeckSlot, PrimaryWeapon, RangedWeapon, Material, Treasure, Belt };
