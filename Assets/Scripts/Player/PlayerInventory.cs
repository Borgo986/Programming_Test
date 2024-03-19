using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item represent the current state of an item during the game (data only)
/// </summary>
public class Item
{
    public ItemData itemData;
    public int quantity;

    public Item()
    {
        this.itemData = null;
        this.quantity = 0;
    }

    public Item(ItemData itemData, int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;
    }
}

public class PlayerInventory : MonoBehaviour
{
    public int inventory_size = 28;
    [HideInInspector] public List<Item> item_list = new List<Item>();

    private static PlayerInventory instance;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        //if player press tab open inventory
        if (Input.GetKeyDown(KeyCode.Tab))
            InventoryPanel.Get().ToggleInventoryPanel();
    }

    //add new item in the item list or add quantity if already exist
    public bool AddItem(Item item)
    {
        if(item.itemData.isStackable)
        {
            //existing item in inventory
            foreach (Item inv_item in item_list) 
            {
                if (inv_item.itemData.id == item.itemData.id)
                {
                    inv_item.quantity += item.quantity;
                    return true;
                }
            }
        }

        //new item in inventory
        if (item_list.Count < inventory_size)
        {
            item_list.Add(item);
            return true;
        }else
            return false;
    }

    //remove item quantity or remove it entirely
    public bool RemoveItem(Item item_to_remove, int quantity)
    {
        foreach(Item item in item_list) 
        {
            if(item.itemData.id == item_to_remove.itemData.id) 
            {
                if(quantity <= item.quantity)
                    item.quantity -= quantity;
                else
                {
                    Debug.LogError("not enough items to remove");
                    return false;
                }

                //removed all item's quantity
                if(item.quantity <= 0)
                    item_list.Remove(item);

                return true;
            }
        }
     
        return false;
    }

    public List<Item> GetPlayerItems()
    {
        return item_list;
    }

    public static PlayerInventory Get()
    {
        return instance;
    }
}
