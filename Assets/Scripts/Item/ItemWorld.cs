using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public ItemData itemData;
    public int quantity = 1;
    private bool isPickable = false;

    private void Update()
    {
        if (isPickable && Input.GetKeyDown("f"))
        {
            Debug.Log("Item collected");
            PickUpItem();
        }
    }

    public void PickUpItem()
    {
        if (PlayerInventory.Get() == null)
            return;

        //create the item and pass it to the inventory
        Item item = new Item(itemData, quantity);
        PlayerInventory.Get().AddItem(item);

        //Show the preview of the obtained item
        PromptPanel.Get().ShowPreviewForSeconds(item, 3f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //show prompt panel
            PromptPanel.Get().ShowInteractionPanel();
            isPickable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //hide prompt panel
            PromptPanel.Get().HideInteractionPanel();
            isPickable = false;
        }
    }

}
