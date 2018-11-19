using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateButton : MonoBehaviour
{

    public CheckSceneScript Cs;
    private Button button;
    public Trip Commute;

    private void Awake()
    {
        Cs = FindObjectOfType<CheckSceneScript>();
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Cs.Prompt(this);
    }
}
