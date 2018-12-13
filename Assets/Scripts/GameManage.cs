using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour {

    public GameObject playerObject;
    public GameObject UI;
    public GameObject Camera;


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

        //Ifthe player pos drops below 0, spawn him on 0,0,1
        if (playerObject.transform.position.y < 0)
        {
            playerObject.transform.position = new Vector3(0f, 1f, 0f);
        }

        //Check if inventory has been opened
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.B))
        {
            //If the screen is hidden, unhide it
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                UI.GetComponent<CanvasGroup>().alpha = 1.0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                //Set the location of the camera a bit to the right so that the player is in view
                Transform pivot = playerObject.GetComponent<PlayerMovement>().PivotTransform.transform;
                    //Get the distance the camera is away from the player
                float currentDistance = Camera.GetComponentInChildren<CameraCollision>().Distance;           
                pivot.position = pivot.position + pivot.right * 0.85f * currentDistance;

                //Disable the player form moving and using camera
                Camera.GetComponent<CollidingCamera>().IsAllowedToUpdate = false;
                playerObject.GetComponent<PlayerMovement>().enabled = false;
                Camera.GetComponentInChildren<CameraCollision>().IsAllowedToUpdate = false;


            }

            //If the screen is shown, hide it
            else if (Cursor.lockState == CursorLockMode.None)
            {

                //Allow the player to move again and use the camera  
                playerObject.GetComponent<PlayerMovement>().enabled = true;
                Camera.GetComponent<CollidingCamera>().IsAllowedToUpdate = true;
                Camera.GetComponentInChildren<CameraCollision>().IsAllowedToUpdate = true;
                //Get the distance the camera is away from the player
                Transform pivot = playerObject.GetComponent<PlayerMovement>().PivotTransform.transform;
                float currentDistance = Camera.GetComponentInChildren<CameraCollision>().Distance;
                pivot.position = pivot.position - pivot.right * 0.85f * currentDistance;
                UI.GetComponent<CanvasGroup>().alpha = 0.0f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;




            }
        }
    }
}
