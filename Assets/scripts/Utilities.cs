using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{

    /// <summary>
    /// Parses time from string table containing either single 2, 3 or 4 long entry or two 1-2 long entries. Throws Exception if parses fail or given information is wrong.
    /// </summary>
    /// <param name="times">Table of one or two strings containing time information.</param>
    /// <returns>Time in string format "hh:mm".</returns>
    public static string ParseTime(string[] times)
    {
        string parsedTime = "";

        // If there is only one entry in the string table, try to parse it 
        if (times.Length == 1)
        {
            // If there is only 2 characters, treat the string as if the user would like to pass an even hour.
            if (times[0].Length == 2)
            {
                // Save the two characters to time string to be parsed as the hour.
                string time = "" + times[0][0] + times[0][1];
                // Make sure that time is in the right format.
                int.Parse(time);
                parsedTime += time + ":00";
            }
            // If there are 3 characters, treat the string as if the user would like to pass time in a h:mm format.
            else if (times[0].Length == 3)
            {
                // Save the first character to a string to be parsed as the hour.
                string time = "" + times[0][0];
                // Make sure that time is in the right format.
                int.Parse(time);
                parsedTime += time + ":";
                // Save the last two character to a string to be parsed as minutes.
                time = "" + times[0][1] + times[0][2];
                int.Parse(time);
                parsedTime += time;
            }
            // If there are 4 characters, parse time in hh:mm format.
            else if (times[0].Length == 4)
            {
                // Save the first two characters to a string to be parsed as the hour.
                string time = "" + times[0][0] + times[0][1];
                int.Parse(time);
                parsedTime += time + ":";
                // Save the last two character to a string to be parsed as minutes.
                time = "" + times[0][2] + times[0][3];
                int.Parse(time);
                parsedTime += time;
            }
            // If method was called with only a single string entry to the table and that string was less than 2 characters or more than 4 characters wrong, throw error.
            else
            {
                throw new UnityException();
            }
        }
        // If there are two entries to the string table, treat the first entry as the hour and the second entry as the minutes.
        else if (times.Length == 2)
        {
            if (times[0].Length <= 2 && times[1].Length <= 2)
            {
                //make sure that the entries are in the right format.
                int.Parse(times[0]);
                int.Parse(times[1]);
                parsedTime += times[0] + ":" + times[1];
            }
            // If there are too many numbers in either entry, throw error.
            else
            {
                throw new UnityException();
            }
        }
        //if string table contains 0 or more than 2 entries, throw error.
        else
        {
            throw new UnityException();
        }
        return parsedTime;
    }
}
