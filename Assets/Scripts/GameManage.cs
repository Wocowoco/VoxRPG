using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour {

    public GameObject playerObject;
    public GameObject UI;


    //Make it so there's only one inventoryScript
    private static GameManage instance;

    public static GameManage MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManage>();
            }

            return instance;
        }
    }

    // Use this for initialization
    void Start() {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {


        //Check if inventory has been opened
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.B))
        {
            //If the screen is hidden, unhide it
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                UI.GetComponent<CanvasGroup>().alpha = 1.0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            //If the screen is shown, hide it
            else if (Cursor.lockState == CursorLockMode.None)
            {
                UI.GetComponent<CanvasGroup>().alpha = 0.0f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
