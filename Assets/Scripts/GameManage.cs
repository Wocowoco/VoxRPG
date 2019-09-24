using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManage : MonoBehaviour {

    public GameObject playerObject;
    public GameObject InventoryScreenObject;
    public GameObject CameraObject;

    private bool isAimLocked = false;
    private float aimLockedTime = 0.0f;
    private float aimLockedTotal = 0.0f;


    //Make it so there's only one GameManage
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

        //Start the controllers
        DamageTextController.Initialize();
    }

    // Update is called once per frame
    void Update() {

        //If the player pos drops below 0, spawn him on 0,0,1
        if (playerObject.transform.position.y < 0)
        {
            playerObject.transform.position = new Vector3(0f, 1f, 0f);
        }

        //Check if inventory has been opened
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.B))
        {
            //-------------
            //SHOWING UI
            //-------------
            //If the screen is hidden, unhide it
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                InventoryScreenObject.GetComponent<CanvasGroup>().alpha = 1;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                //Set the location of the camera a bit to the right so that the player is in view
                //Transform pivot = playerObject.GetComponent<PlayerMovement>().PivotTransform.transform;
                //Get the distance the camera is away from the player
                //float currentDistance = Camera.GetComponentInChildren<CameraCollision>().Distance;           
                //pivot.position = pivot.position + pivot.right * 0.85f * currentDistance;
                CameraObject.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 0.5f, 1);

                //Disable the player form moving and using camera
                CameraObject.GetComponent<CollidingCamera>().IsAllowedToUpdate = false;
                playerObject.GetComponent<PlayerMovement>().IsAllowedToUpdate = false;
                CameraObject.GetComponentInChildren<CameraCollision>().IsAllowedToUpdate = false;
                playerObject.GetComponentInChildren<PlayerAction>().IsAllowedToUpdate = false;



            }
            //-----------
            //HIDE UI
            //-----------
            //If the screen is shown, hide it
            else if (Cursor.lockState == CursorLockMode.None)
            {

                //Allow the player to move again and use the camera  
                playerObject.GetComponent<PlayerMovement>().IsAllowedToUpdate = true;
                playerObject.GetComponentInChildren<PlayerAction>().IsAllowedToUpdate = true;
                CameraObject.GetComponent<CollidingCamera>().IsAllowedToUpdate = true;
                CameraObject.GetComponentInChildren<CameraCollision>().IsAllowedToUpdate = true;
                //Get the distance the camera is away from the player
                //Transform pivot = playerObject.GetComponent<PlayerMovement>().PivotTransform.transform;
                //float currentDistance = Camera.GetComponentInChildren<CameraCollision>().Distance;
                //pivot.position = pivot.position - pivot.right * 0.85f * currentDistance;
                CameraObject.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 1, 1);
                InventoryScreenObject.GetComponent<CanvasGroup>().alpha = 0;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;


            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        
    }

    private void LateUpdate()
    {
        //When the player is aimlocked, it needs to keep looking at the camera directed for a set amount of time;
        if (isAimLocked)
        {
            //Add frametime to locked time
            aimLockedTime += Time.deltaTime;

            //Keep looking at camera
            playerObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Slerp(playerObject.transform.GetChild(0).gameObject.transform.rotation, new Quaternion(0, CameraObject.transform.rotation.y, 0, CameraObject.transform.rotation.w), 75 * Time.deltaTime);
            //As long as the time is less than aimlocked time, keep it locked
            if (aimLockedTime >= aimLockedTotal)
            {
                isAimLocked = false;
                aimLockedTotal = 0.0f;
                aimLockedTime = 0.0f;
            }
        }
    }

    public void FacePlayerTowardsAim(float timeToKeepLooking)
    {
        //Make sure to update the child rotation, since that one changes depending on where the player is moving
        playerObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Slerp(playerObject.transform.GetChild(0).gameObject.transform.rotation, new Quaternion(0,CameraObject.transform.rotation.y,0, CameraObject.transform.rotation.w), 75 * Time.deltaTime);
        isAimLocked = true;
        aimLockedTotal = timeToKeepLooking;
    }


}
