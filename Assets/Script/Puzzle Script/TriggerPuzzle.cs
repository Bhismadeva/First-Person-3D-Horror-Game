using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Puzzle : MonoBehaviour
{
    public Camera playerCamera;
    public Camera doorCamera;
    public GameObject ItemBoxCanvas;

    private void Start()
    {
        // Ensure only one camera is active at start
        playerCamera.gameObject.SetActive(true);
        doorCamera.gameObject.SetActive(false);
        ItemBoxCanvas.gameObject.SetActive(false);
    }
    private void Update()
    {
        // Switch cameras when the user presses the "C" key
        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchCameras();
        }
    }

    private void SwitchCameras()
    {
        if (playerCamera.gameObject.activeSelf)
        {
            playerCamera.gameObject.SetActive(false);
            doorCamera.gameObject.SetActive(true);
            ItemBoxCanvas.gameObject.SetActive(true);

            // Activate Cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Debug.Log("Switch Cameras");
        }
        else
        {
            playerCamera.gameObject.SetActive(true);
            doorCamera.gameObject.SetActive(false);
            ItemBoxCanvas.gameObject.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
