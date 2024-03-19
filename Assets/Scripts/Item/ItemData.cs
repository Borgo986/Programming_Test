using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Generic Item", order = 1)]
public class ItemData : ScriptableObject
{
    public string id;
    public string item_name;
    public Sprite item_icon;

    [Space]
    [TextArea(1 ,10)]
    public string desc;

    [Header("Inventory Stuff")]
    public bool isStackable = true;
    public bool destroyAfterUse = false;

    public virtual void UseItem()
    {
        //overridden method
    }
}

