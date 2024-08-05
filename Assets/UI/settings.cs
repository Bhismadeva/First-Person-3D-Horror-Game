using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settings : MonoBehaviour
{
    [SerializeField] Slider volumeslider;
    [SerializeField] Slider sesitivityslider;
    
    void Start()
    {
        //set the value of volume based on playerpref
        if (!PlayerPrefs.HasKey("volumeValue"))
        {
            //set value to default if player dont have playerpref of volume value
            PlayerPrefs.SetFloat("volumeValue", 0.5f);
            load();
        }
        else
        {
            //if player have change the volume slider before, then load the previous volume value
            load();
        }

        //set the value of sensitivity based on playerpref
        if (!PlayerPrefs.HasKey("sensitivityValue"))
        {
            //set value to default if player dont have playerpref of sensitivity value
            PlayerPrefs.SetFloat("sensitivityValue", 0.5f);
            loadsen();
        }
        else
        {
            //if player have change the sensitivity slider before, then load the previous sensitivity value
            loadsen();
        }
    }

    //save the volume value on playerpref if change
    public void changevolume()
    {
        //set the volume listener based on volume slider value
        AudioListener.volume = volumeslider.value;
        save();
    }
    public void load()
    {
        //get the volume slider value from playerpref
        volumeslider.value = PlayerPrefs.GetFloat("volumeValue");
    }
    private void save()
    {
        //set the playerpref volume value based on the value of the volume slider
        PlayerPrefs.SetFloat("volumeValue", volumeslider.value);
    }
    //end of the volume value settings

    //save the sensitivity value on playerpref if change
    public void changesensitivity()
    {
        PlayerPrefs.SetFloat("sensitivityValue", sesitivityslider.value);
        savesen();
    }
    public void loadsen()
    {
        sesitivityslider.value = PlayerPrefs.GetFloat("sensitivityValue");
    }
    private void savesen()
    {
        PlayerPrefs.SetFloat("sensitivityValue", sesitivityslider.value);
    }
    //end of the sensitivity value settings
}
