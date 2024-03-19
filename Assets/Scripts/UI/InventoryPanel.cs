using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : UIPanel
{
    public GameObject inv_content;
    public GameObject itemUI_prefab;
    public ThirdPersonCam cinemachine_camera;
    
    [Header("Interaction Panel")]
    public UIPanel interaction_panel;
    public RectTransform interaction_spawn_point;

    [Header("Discard Panel")]
    public UIPanel discard_panel;
    public TextMeshProUGUI discard_title;
    public TMP_InputField discard_quantity;
    public Slider discard_slider;

    private int old_inputfield_value = 0;
    private bool inputfield_selected = false;

    private List<InventorySlot> inventory_list;
    private Item selected_item;

    private static InventoryPanel instance;

    protected override void Start()
    {
        base.Start();

        if (instance == null)
            instance = this;

        if(inv_content != null)
            inventory_list = inv_content.GetComponentsInChildren<InventorySlot>().ToList();

        if (discard_title != null)
            discard_title.text = "";

        if (discard_quantity != null)
            discard_quantity.text = "0";

        if (discard_slider != null)
        {
            discard_slider.maxValue = 100;
            discard_slider.value = 0;
        }
    }

    //Clear the current inventory and repopulate it
    public void RefreshInventory()
    {
        ClearInventory();

        int index = 0;
        foreach (Item item in PlayerInventory.Get().item_list) 
        {
            inventory_list[index].SetItem(item);
            index++;
        }

    }

    public void ClearInventory()
    {
        foreach (InventorySlot slot in inventory_list)
        {
            slot.Clear();
        }
    }

    //Show and Hide the inventory
    public void ToggleInventoryPanel()
    {
        if(!IsVisible())
        {
            RefreshInventory();
            //disable camera rotation and enable mouse
            cinemachine_camera.DisableCameraRotation();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Show();
            
        }
        else
        {
            //enable camera rotation and disable mouse
            cinemachine_camera.EnableCameraRotation();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            HideAllInventoryPanels();
        }
    }

    public void ShowInteractionPanel(Vector3 pos, Item item)
    {
        if (interaction_panel == null || interaction_spawn_point == null)
            return;

        //set the selected item
        selected_item = item;

        //set the position of the interaction panel
        interaction_spawn_point.position = pos;
        interaction_panel.Show();
    }

    public void HideInteractionPanel()
    {
        if (interaction_panel == null || interaction_spawn_point == null)
            return;

        //clear selected item
        selected_item = null;
        interaction_panel.Hide();
    }

    public void HideAllInventoryPanels()
    {
        InspectorPanel.Get().Hide();

        HideInteractionPanel();
        Hide();
    }

    //set values of dicard panel according to the object
    public void ShowDiscardPanel(Item itemToDiscard)
    {
        discard_title.text = $"Discard x{itemToDiscard.quantity} {itemToDiscard.itemData.item_name}?";
        discard_quantity.text = itemToDiscard.quantity.ToString();
        discard_slider.maxValue = itemToDiscard.quantity;
        discard_slider.value = itemToDiscard.quantity;
        discard_panel.Show();
    }

    #region On value changed in discard panel
    //on value changed of slider on discard panel
    public void OnDiscardSliderValueChanged()
    {
        if(selected_item != null)
            discard_title.text = $"Discard x{discard_slider.value} {selected_item.itemData.item_name}?";
        discard_quantity.text = discard_slider.value.ToString();
        old_inputfield_value = (int) discard_slider.value;
    }

    //on select input field
    public void OnDiscardInputFieldSelect()
    {
        inputfield_selected = true;
    }

    //on deselect input field
    public void OnDiscardInputFieldDeselect()
    {
        inputfield_selected = false;
    }

    //on value changed of input field on discard panel
    public void OnDiscardInputFieldValueChanged()
    {
        if (inputfield_selected && selected_item != null)
        {
            int.TryParse(discard_quantity.text, out int value);
            bool isValid = value >= 0 && value <= selected_item.quantity;
            if (isValid)
            {
                discard_title.text = $"Discard x{value} {selected_item.itemData.item_name}?";
                discard_slider.value = value;
            }
            else
                discard_quantity.text = old_inputfield_value.ToString();
        }
    }
    #endregion

    #region OnClickEvents
    public void OnClickOpenInspector()
    {
        if (selected_item == null)
            return;

        InspectorPanel.Get().ShowInspector(selected_item.itemData);
        interaction_panel.Hide(true);
    }

    public void OnClickUseItem()
    {
        if(selected_item == null) 
            return;

        //use and remove item from inventory
        selected_item.itemData.UseItem();
        if (selected_item.itemData.destroyAfterUse)
        {
            PlayerInventory.Get().RemoveItem(selected_item, 1);
            RefreshInventory();
        }

        interaction_panel.Hide(true);
    }

    public void OnClickDiscardItem()
    {
        if (selected_item == null)
            return;

        //remove item from inventory and refresh it
        PlayerInventory.Get().RemoveItem(selected_item, (int)discard_slider.value);
        RefreshInventory();
        discard_panel.Hide();
    }

    public void OnClickOpenDiscardPanel()
    {
        if (selected_item == null)
            return;

        ShowDiscardPanel(selected_item);
        interaction_panel.Hide(true);
    }
    #endregion

    public static InventoryPanel Get()
    {
        return instance;
    }

}
