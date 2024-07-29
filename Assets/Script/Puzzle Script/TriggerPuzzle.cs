using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public Camera playerCamera;
    public Camera doorCamera;
    public GameObject puzzleBoxCanvas;
    private bool isPlayerInRange = false;
    public GameObject RPopUp;

    private void Start()
    {
        // Ensure only one camera is active at start
        playerCamera.gameObject.SetActive(true);
        doorCamera.gameObject.SetActive(false);
        puzzleBoxCanvas.gameObject.SetActive(false);
    }
    private void Update()
    {
        // Switch cameras when the user presses the "C" key
        if (Input.GetKeyDown(KeyCode.F) && isPlayerInRange)
        {
            SwitchCameras();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            RPopUp.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            RPopUp.SetActive(false);
        }
    }

    private void SwitchCameras()
    {
        if (playerCamera.gameObject.activeSelf)
        {
            playerCamera.gameObject.SetActive(false);
            doorCamera.gameObject.SetActive(true);
            puzzleBoxCanvas.gameObject.SetActive(true);

            // Activate Cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Debug.Log("Switch Cameras");
        }
        else
        {
            playerCamera.gameObject.SetActive(true);
            doorCamera.gameObject.SetActive(false);
            puzzleBoxCanvas.gameObject.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
