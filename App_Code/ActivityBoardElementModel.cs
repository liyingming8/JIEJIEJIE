
/// <summary>
/// ActivityBoardElementModel 的摘要说明
/// </summary>
public class ActivityBoardElementModel
{
    public ActivityBoardElementModel()
	{
        Halign = "";
        Valign = "";
        Elementtype = 1;
        Keyvalue = "";
        Elementdiscription = "";
        Content = "";
        Maxheight = 0;
	    Maxwidth = 0; 
	}

    public int Elementtype { get; set; }

    public string Keyvalue { get; set; }

    public string Elementdiscription { get; set; }

    public string Content { get; set; }

    public string Halign { get; set; }

    public string Valign { get; set; }

    public int Maxwidth { get; set; }

    public int Maxheight { get; set; }
}