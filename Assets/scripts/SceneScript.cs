using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{

    public void Return()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ToSaveScene()
    {
        SceneManager.LoadScene("SaveScene");
    }

    public void ToCheckScene()
    {
        SceneManager.LoadScene("CheckScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
