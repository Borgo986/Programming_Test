using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image item_img;
    public TextMeshProUGUI quantity;
    public Item current_item;
    public Image slotImage;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public bool changedSlot = false;

    private void Start()
    {
        if (slotImage == null)
            Debug.LogError("slotImage not assigned");

        Clear();
    }

    //set item graphics
    public void SetItem(Item item)
    {
        item_img.sprite = item.itemData.item_icon;
        //set the alpha at 1
        Color color = item_img.color;
        color.a = 1;
        item_img.color = color;
        quantity.text = item.quantity > 1 ? item.quantity.ToString() : "";
        current_item = item;
        slotImage.raycastTarget = true;
    }

    //clear item graphics
    public void Clear()
    {
        item_img.sprite = null;
        Color color = item_img.color;
        color.a = 0;
        item_img.color = color;
        quantity.text = "";
        current_item = null;
        changedSlot = false;
        slotImage.raycastTarget = false;
    }

    //on right click open interaction panel
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(current_item != null)
            {
                //open interact panel
                InventoryPanel.Get().ShowInteractionPanel(Input.mousePosition, current_item);
            }
        }
    }

    //on begin drag change item transform for a better UI view
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        slotImage.raycastTarget = false;
    }

    //on drag move item icon
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }  

    //on end drag clear slot if changed position
    public void OnEndDrag(PointerEventData eventData)
    {
        if (changedSlot)
            Clear();
        else
            slotImage.raycastTarget = true;

        transform.SetParent(parentAfterDrag);
    }   
}
