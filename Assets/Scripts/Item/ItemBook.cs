using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Book", order = 2)]
public class ItemBook : ItemData
{
    [Header("Book Values")]
    [Header("Don't exceed the line limit, use another chapter")]
    [TextArea(5, 5)]
    public List<string> book_text;

    public override void UseItem()
    {
        base.UseItem();

        //show book panel
        if(BooksPanel.Get() != null)
            BooksPanel.Get().ShowBookPanel(book_text);
    }

}
