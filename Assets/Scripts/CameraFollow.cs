using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform PivotTransform;
    public Vector3 CameraOffset;
    public float HeightOffset;
    public float RotationSpeed;
    public float HighestAngle;
    public float LowestAngle;




	// Use this for initialization
	void Start () {
        CameraOffset = transform.position - PivotTransform.position;

        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
              //Change Max Zoom based on scroll input
        if (Input.GetAxis("Mouse Scrollwheel") != 0)
        {

        }
    }

    void LateUpdate()
    {
       
        //Get x pos of mouse, and rotate player accordingly
        float horizontal = Input.GetAxis("Mouse X") * RotationSpeed;
        PivotTransform.Rotate(0f, horizontal, 0f);

        //Move camera according to rotation of player and its offset
        float desiredYAngle = PivotTransform.eulerAngles.y;
        float desiredXAngle = PivotTransform.eulerAngles.x;

        //Get y pos of mouse, and turn camera (using the pivot of player, not the player itself)
        float vertical = Input.GetAxis("Mouse Y") * RotationSpeed;



        PivotTransform.Rotate(-vertical, 0f, 0f);

        //Clamp the camera rotation (up/down)
        if (PivotTransform.rotation.eulerAngles.x > HighestAngle && PivotTransform.rotation.eulerAngles.x < 180f)
        {
            PivotTransform.rotation = Quaternion.Euler(HighestAngle, desiredYAngle, 0f);
        }

        if (PivotTransform.rotation.eulerAngles.x > 180f && PivotTransform.rotation.eulerAngles.x < 360f + LowestAngle)
        {
            PivotTransform.rotation = Quaternion.Euler(360f + LowestAngle, desiredYAngle, 0f);
        }
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        Vector3 newPos = PivotTransform.position + (rotation * CameraOffset);
        transform.position = Vector3.Slerp(transform.position, newPos, 100f * Time.deltaTime);
        transform.LookAt(new Vector3(PivotTransform.position.x, (PivotTransform.position.y + HeightOffset), PivotTransform.position.z));
    }
}
