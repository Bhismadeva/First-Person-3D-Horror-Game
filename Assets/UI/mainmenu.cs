using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    [SerializeField] Button continuebutton;

    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volumeValue");

        Cursor.lockState = CursorLockMode.Confined;

        //check if the game is saved
        if (!PlayerPrefs.HasKey("save"))
        {
            //set the button to not interactable if the game doesnt have saved file
            continuebutton.interactable = false;
        }
        else
        {
            //set the button to interactable if the game have saved file
            continuebutton.interactable = true;
        }
    }
    //load the gave if saved before
    public void continuegame()
    {
        //load playerpref, have fun on coding this
    }

    //start new game
    public void newgame()
    {
        //change to play first scene, have fun on coding this
        SceneManager.LoadScene("MainScene");
    }

    //exit
    public void exit()
    {
        Application.Quit();
    }
}
