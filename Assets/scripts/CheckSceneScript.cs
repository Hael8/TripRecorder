using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckSceneScript : SceneScript
{

    public GameObject DatePrefab;
    public Transform Content;

    public GameObject DestroyPrompt;
    public GameObject PromptWindow;
    public GameObject EditPrompt;

    private DateButton editDate;
    private List<Trip> savedDates;

    private string destroyPromptMessage;

    public InputField Day;
    public InputField Month;
    public InputField Location;
    public InputField StartTime;
    public InputField EndTime;

    void Awake()
    {
        //Read all the saved dates:
        savedDates = new List<Trip>();
        destroyPromptMessage = "Are you sure you want to remove this trip?";
        try
        {
            using (StreamReader r = new StreamReader(Application.persistentDataPath + "/data"))
            {
                while (r.Peek() >= 0)
                {
                    string line = r.ReadLine();
                    string[] data = line.Split(new char[] { '.' });
                    if (data.Length == 5)
                        savedDates.Add(new Trip(int.Parse(data[0]), int.Parse(data[1]), data[2], data[3], data[4]));
                    else
                        savedDates.Add(new Trip(0, 0, "Error", "00:00", "99:99"));
                }
                r.Close();
            }
        }
        catch
        {
            Debug.LogError("Error while reading line");
        }
        foreach (Trip line in savedDates)
        {
            GameObject newDate = Instantiate(DatePrefab, Content);
            newDate.GetComponent<DateButton>().Commute = line;
            newDate.GetComponentInChildren<Text>().text = line.ToString();
        }
    }

    public void PromptToDestroy()
    {
        PromptWindow.SetActive(false);
        DestroyPrompt.SetActive(true);
        DestroyPrompt.GetComponentInChildren<Text>().text = destroyPromptMessage + "\n\n" + editDate.GetComponentInChildren<Text>().text;
    }

    public void PromptToEdit()
    {
        EditPrompt.SetActive(true);
        PromptWindow.SetActive(false);
    }

    public void Prompt(DateButton dateButton)
    {
        foreach (Button b in GetComponentsInChildren<Button>())
        {
            b.interactable = false;
        }
        PromptWindow.SetActive(true);
        PromptWindow.GetComponentInChildren<Text>().text = dateButton.Commute.ToString();
        editDate = dateButton;
    }

    public void No()
    {
        foreach (Button b in GetComponentsInChildren<Button>())
        {
            b.interactable = true;
        }
        DestroyPrompt.SetActive(false);
    }

    public void Cancel()
    {
        foreach (Button b in GetComponentsInChildren<Button>())
        {
            b.interactable = true;
        }
        EditPrompt.SetActive(false);
        PromptWindow.SetActive(false);
    }

    public void Edit()
    {
        EditPrompt.GetComponentsInChildren<Text>()[0].text = editDate.Commute.ToString();
        EditPrompt.SetActive(true);
        PromptWindow.SetActive(false);
    }

    public void Yes()
    {
        foreach (Button b in GetComponentsInChildren<Button>())
        {
            b.interactable = true;
        }
        savedDates.Remove(editDate.Commute);
        using (StreamWriter w = new StreamWriter(Application.persistentDataPath + "/data", false))
        {
            foreach (Trip c in savedDates)
            {
                w.WriteLine(c.GetSaveForm());
            }
            w.Close();
        }
        Destroy(editDate.gameObject);
        DestroyPrompt.SetActive(false);
    }

    public void Save()
    {
        try
        {
            bool informationChanged = false;
            string editText = Day.text;
            Day.text = "";
            if (editText != "")
            {
                editDate.Commute.ChangeDay(int.Parse(editText));
                informationChanged = true;
            }
            editText = Month.text;
            Month.text = "";
            if (editText != "")
            {
                editDate.Commute.ChangeMonth(int.Parse(editText));
                informationChanged = true;
            }
            editText = Location.text;
            Location.text = "";
            if (editText != "")
            {
                editDate.Commute.ChangeLocation(editText);
                informationChanged = true;
            }
            editText = StartTime.text;
            StartTime.text = "";
            try
            {
                string[] times = editText.Split(new char[] { '.', ' ', ',', ':', ';', '-' }, System.StringSplitOptions.RemoveEmptyEntries);
                editText = Utilities.ParseTime(times);
                editDate.Commute.ChangeStartTime(editText);
                informationChanged = true;
            }
            catch
            {
                Debug.Log("Error");
            }
            editText = EndTime.text;
            EndTime.text = "";
            try
            {
                string[] times = editText.Split(new char[] { '.', ' ', ',', ':', ';', '-' }, System.StringSplitOptions.RemoveEmptyEntries);
                editText = Utilities.ParseTime(times);
                editDate.Commute.ChangeEndTime(editText);
                informationChanged = true;
            }
            catch
            {
                Debug.Log("Error");
            }
            if (informationChanged)
            {
                editDate.GetComponentInChildren<Text>().text = editDate.Commute.ToString();
                foreach (Trip commute in savedDates)
                {
                    using (StreamWriter w = new StreamWriter(Application.persistentDataPath + "/data", false))
                    {
                        foreach (Trip c in savedDates)
                        {
                            w.WriteLine(c.GetSaveForm());
                        }
                        w.Close();
                    }
                }
            }
            foreach (Button b in GetComponentsInChildren<Button>())
            {
                b.interactable = true;
            }
            EditPrompt.SetActive(false);
        }
        catch
        {
            Debug.Log("Error!");
        }
    }
}
