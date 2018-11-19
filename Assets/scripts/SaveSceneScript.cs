using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSceneScript : SceneScript
{

    public Text date;
    public Text saveFeedback;

    public InputField startTime;
    public InputField endTime;
    public InputField location;

    private string day;
    private string month;

    // Use this for initialization
    void Start()
    {
        date.text = System.DateTime.Now.Day + "." + System.DateTime.Now.Month + "." + System.DateTime.Now.Year;
        day = System.DateTime.Now.Day.ToString();
        month = System.DateTime.Now.Month.ToString();

        saveFeedback.text = "";
    }

    public void SaveDate()
    {
        string sTime = startTime.text;
        string eTime = endTime.text;
        string locationText = location.text;
        string saveData = "";
        saveData += day + "." + month + "." + locationText + ".";

        try
        {
            string[] times = sTime.Split(new char[] { '.', ' ', ',', ':', ';', '-' }, System.StringSplitOptions.RemoveEmptyEntries);

            saveData += Utilities.ParseTime(times) + ".";
            
            times = eTime.Split(new char[] { '.', ' ', ',', ':', ';', '-' }, System.StringSplitOptions.RemoveEmptyEntries);

            saveData += Utilities.ParseTime(times);

            using (StreamWriter w = new StreamWriter(Application.persistentDataPath + "/data", true))
            {
                w.WriteLine(saveData);
                w.Close();
            }
            saveFeedback.text = "Tallennus onnistui!";
            startTime.text = "";
            endTime.text = "";
            location.text = "";
            SceneManager.LoadScene("CheckScene");
        }
        catch
        {
            saveFeedback.text = "Epäonnistui!";
        }
    }
}
