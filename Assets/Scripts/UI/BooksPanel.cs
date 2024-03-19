using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BooksPanel : UIPanel
{
    public TextMeshProUGUI text_pt1;
    public TextMeshProUGUI text_pt2;

    private int current_index = 0;
    private List<string> current_book = new List<string>();

    private static BooksPanel instance;

    protected override void Start()
    {
        if (instance == null)
            instance = this;

        base.Start();

        Clean();
    }

    //clean panel
    public void Clean()
    {
        current_index = 0;
        current_book.Clear();
        CleanBookText();
    }

    //clean texts
    public void CleanBookText()
    {
        if (text_pt1 != null)
            text_pt1.text = "";
        if (text_pt2 != null)
            text_pt2.text = "";
    }

    //set first chapter of the text
    public void SetBookText(List<string> chapter_list)
    {
        CleanBookText();

        if (chapter_list.Count > 0)
        {
            current_book = chapter_list;
            text_pt1.text = chapter_list[0];      
        }
        else
        {
            Debug.Log("Empty Book");
            return;
        }
    }

    //Show book Panel
    public void ShowBookPanel(List<string> chapter_list, bool instant = false)
    {
        SetBookText(chapter_list);
        Show(instant);
    }

    //Set the next pages
    public void OnClickNextPage()
    {
        if(current_book.Count > current_index + 1)
        {
            CleanBookText();
            current_index += 2;
            text_pt2.text = current_book[current_index - 1];
            if(current_book.Count > current_index)
                text_pt1.text = current_book[current_index];
        }
    }

    //Set the previous pages
    public void OnClickPreviousPage()
    {
        if (current_index <= 0)
            return;

        CleanBookText();
        current_index -= 2;
        text_pt1.text = current_book[current_index];
        if(current_index - 1 >= 0)
            text_pt2.text = current_book[current_index - 1];
    }
    
    public static BooksPanel Get()
    {
        return instance;
    }

}
