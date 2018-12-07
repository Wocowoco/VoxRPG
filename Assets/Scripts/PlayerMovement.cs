using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float MoveSpeed;
    public float JumpForce;

    public Transform PivotTransform;
    public float RotationSpeed;
    public GameObject PlayerModel;

    public Animator PlayerAnimations;

    private Vector3 moveDirection;
    private CharacterController playerController;

    [HideInInspector]
    public float PivotYOffset;


  

    // Use this for initialization
    void Start()
    {
        playerController = GetComponent<CharacterController>();

        //Make it so the Pivot follows the player, but doesn't rotate with it.
        PivotYOffset = PivotTransform.localPosition.y * this.transform.localScale.y;
        PivotTransform.transform.position = new Vector3(transform.position.x, transform.position.y + PivotYOffset, transform.position.z);
        PivotTransform.transform.parent = null;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Update Pivot's position
        PivotTransform.transform.position = new Vector3(transform.position.x, transform.position.y + PivotYOffset, transform.position.z);
        PlayerModel.transform.rotation = PlayerModel.transform.rotation;

        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * MoveSpeed, moveDirection.y, Input.GetAxis("Vertical") * MoveSpeed);
        float yValue = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal") * MoveSpeed);
        moveDirection = moveDirection.normalized * MoveSpeed;
        moveDirection.y = yValue;

        //Jump if on the ground
        if (playerController.isGrounded == true)
        {
            moveDirection.y = 0;
            if (Input.GetKey(KeyCode.Space))
            {
                moveDirection.y = JumpForce;
            }
        }
 

        moveDirection.y = moveDirection.y + (Physics.gravity.y * Time.deltaTime);
        playerController.Move(moveDirection * Time.deltaTime);


        //Move the player in the direction of the camera
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, PivotTransform.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));          
            PlayerModel.transform.rotation = Quaternion.Slerp(PlayerModel.transform.rotation, newRotation, RotationSpeed * Time.deltaTime);
        }


        //Update Animations
        PlayerAnimations.SetBool("isGrounded", playerController.isGrounded);
        if (Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f)
        {
            PlayerAnimations.SetFloat("Speed", 1f);
        }
        else
        {
            PlayerAnimations.SetFloat("Speed", 0f);
        }

    }
}
