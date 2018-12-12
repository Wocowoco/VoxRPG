using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingCamera : MonoBehaviour {

    public float CameraMoveSpeed = 120.0f;
    public GameObject CameraFollowObject;
    Vector3 FollowPOS;
    public float ClampAngleTop = 80.0f;
    public float ClampAngleBottom = -20f;
    public float InputSensitivity = 150.0f;
    public GameObject CameraObject;
    public GameObject PlayerObject;
    public Transform PivotPointPlayer;
    public float CamDistanceToPlayerX;
    public float CamDistanceToPlayerY;
    public float CamDistanceToPlayerZ;
    public float MouseX;
    public float MouseY;
    public float FinalInputX;
    public float FinalInputZ;
    public float SmoothX;
    public float SmoothY;
    public bool IsAllowedToUpdate = true;

    private float rotY = 0.0f;
    private float rotX = 0.0f;
    // Use this for initialization
    void Start () {

        //Lock the cursor;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //Get the rotation of the camera
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        //Snap camera to player
        this.transform.position = PlayerObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (IsAllowedToUpdate)
        {
            //float inputX = Input.GetAxis("CONTROLLER STICK NAME HORIZONTAL"); 
            //float inputY= Input.GetAxis("CONTROLLER STICK NAME VERTICAL");
            MouseX = Input.GetAxis("Mouse X");
            MouseY = Input.GetAxis("Mouse Y");
            FinalInputX = MouseX; //Add inputX for controller support
            FinalInputZ = MouseY; //Add inputY for controller support


            //Update Pivot Rotation (To walk with camera axis)
            PivotPointPlayer.rotation = this.transform.rotation;

            rotY += FinalInputX * InputSensitivity * Time.deltaTime;
            rotX += FinalInputZ * InputSensitivity * Time.deltaTime;

            //Prevent camera from spinning all the way to the top or bottom
            rotX = Mathf.Clamp(rotX, -ClampAngleBottom, ClampAngleTop);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
        }
    }

    void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        //Set object to follow
        Transform target = CameraFollowObject.transform;

        //Move towards that target
        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
