using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromptPanel : UIPanel
{
    [Header("Wordl Interaction Panel")]
    public UIPanel world_interaction_panel;

    [Header("Preview Item Panel")]
    public UIPanel world_preview_item_panel;
    public TextMeshProUGUI item_name;
    public TextMeshProUGUI item_quantity;
    public Image item_image;

    private static PromptPanel instance;

    private bool isWaiting = false;
    private float timer = 0f;
    private float timeToWait = 0f;

    protected override void Start()
    {
        base.Start();

        if (instance == null)
            instance = this;

        Show(true);
    }

    protected override void Update()
    {
        base.Update();

        //timer preview item
        if (isWaiting)
        {
            timer += Time.deltaTime;
            if(timer >= timeToWait)
            {
                HidePreviewItemPanel();
                isWaiting = false;
            }
        }
    }

    public void ShowInteractionPanel(bool instant = false)
    {
        //Show(true);
        world_interaction_panel.Show(instant);
    }

    public void HideInteractionPanel(bool instant = false)
    {
        world_interaction_panel.Hide(instant);
    }

    //Set preview item values and show it
    public void ShowPreviewItemPanel(Item item, bool instant = false)
    {
        item_name.text = item.itemData.item_name;
        item_quantity.text = "x" + item.quantity.ToString();
        item_image.sprite = item.itemData.item_icon;

        HideInteractionPanel(true);
        world_preview_item_panel.Show(instant);
    }

    public void HidePreviewItemPanel(bool instant = false)
    {
        world_preview_item_panel.Hide(instant);
    }

    //Show preview panel with a timer for the fade out
    public void ShowPreviewForSeconds(Item item, float seconds, bool instant = false)
    {
        ShowPreviewItemPanel(item, instant);
        //timer not set in a coroutine so i can refresh it
        isWaiting = true;
        timer = 0f;
        timeToWait = seconds;
    }

    public static PromptPanel Get()
    {
        return instance;
    }

}
