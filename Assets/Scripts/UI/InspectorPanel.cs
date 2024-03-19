using TMPro;
using UnityEngine.UI;

public class InspectorPanel : UIPanel
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public Image icon;

    private static InspectorPanel instance;

    protected override void Start()
    {
        base.Start();

        if(instance == null)
            instance = this;

        CleanInspector();
    }

    //Set inspector values
    public void SetInspector(ItemData item)
    {
        CleanInspector();

        if (title != null)
            title.text = item.item_name;

        if (description != null)
            description.text = item.desc;

        if (icon != null)
            icon.sprite = item.item_icon;
    }

    //Clean inspector values
    public void CleanInspector()
    {
        if(title != null)
            title.text = "";

        if(description != null)
            description.text = "";

        if(icon != null)
            icon.sprite = null;
    }

    public void ShowInspector(ItemData item)
    {
        SetInspector(item);
        Show();
    }

    public static InspectorPanel Get()
    {
        return instance;
    }
}
