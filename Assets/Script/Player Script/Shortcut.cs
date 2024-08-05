using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shortcut : MonoBehaviour
{
    public GameObject paus;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            paus.SetActive(true);
        }
    }
}
