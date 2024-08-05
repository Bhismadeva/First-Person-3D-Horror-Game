using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class paus : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void title()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //exit
    public void exit()
    {
        Application.Quit();
    }
}
