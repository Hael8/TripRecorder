using System.Linq;

public class Trip
{

    private string location;

    private int day;
    private int month;

    private string startTime;
    private string endTime;

    public Trip(int d, int m, string l, string s, string e)
    {
        location = l;
        day = d;
        month = m;
        startTime = s;
        endTime = e;
    }

    public override string ToString()
    {
        string toReturn = "<b>" + day.ToString() + "." + month.ToString() + ". ";
        toReturn += location == null ? "" : location.Length == 0 ? "" : location.Length == 1 ? char.ToUpper(location[0]) + "" : char.ToUpper(location[0]) + location.Substring(1);
        toReturn += "</b>, " + startTime + "-" + endTime;
        return toReturn;
    }

    public string GetSaveForm()
    {
        return day.ToString() + "." + month.ToString() + "." + location + "." + startTime + "." + endTime;
    }

    public void ChangeDay(int d)
    {
        day = d;
    }

    public void ChangeMonth(int m)
    {
        month = m;
    }

    public void ChangeLocation(string l)
    {
        location = l;
    }

    public void ChangeStartTime(string s)
    {
        startTime = s;
    }

    public void ChangeEndTime(string e)
    {
        endTime = e;
    }
}
