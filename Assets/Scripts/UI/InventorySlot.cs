using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private ItemUI itemUI;

    private void Start()
    {
        itemUI = GetComponentInChildren<ItemUI>();
    }

    //set item on slot
    public void SetItem(Item item)
    {      
        if(itemUI != null && itemUI.current_item == null)
            itemUI.SetItem(item);
    }

    //clear slot from item
    public void Clear()
    {
        if(itemUI != null)
            itemUI.Clear();
    }

    //On Drop Event change item position
    public void OnDrop(PointerEventData eventData)
    {
        if(itemUI != null && itemUI.current_item == null)
        {
            GameObject dropped = eventData.pointerDrag;
            if(dropped.TryGetComponent(out ItemUI dropped_itemUI) && dropped_itemUI.current_item != null)
            {
                itemUI.SetItem(dropped_itemUI.current_item);
                dropped_itemUI.changedSlot = true;
            }
        }
        
    }
}
