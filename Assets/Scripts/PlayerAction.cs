using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

    private Collider[] hitCollider;
    private bool isAllowedToUpdate = true;


    public bool IsAllowedToUpdate
    {
        set
        {
            isAllowedToUpdate = value;
        }
    }
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        //Only update and take input when allowed to (not in menu)
        if (isAllowedToUpdate)
        {
            //If the player is on the ground and presses F (use)
            if (Input.GetKeyDown(KeyCode.F) && this.transform.parent.GetComponent<CharacterController>().isGrounded)
            {
                hitCollider = Physics.OverlapSphere(transform.position, 0.5f);

                int length = 0;
                while (length < hitCollider.Length)
                {
                    if (hitCollider[length].GetComponent<Interactable>() != null)
                    {
                        Debug.Log("Hit " + hitCollider[length].name);
                        //Run whatever interacting with the object would do
                        hitCollider[length].GetComponent<Interactable>().OnInteract();
                        break;
                    }

                    length++;
                }
            }
        }
    }

    //void OnTriggerStay(Collider otherObject)
    //{
    //    //Check if the collided Object has a Interactible script
    //    if (otherObject.GetComponent<Interactable>() != null)
    //   {

    //        //If the action button is pressed, check if there are any interactibles nearby if not in the air
    //        if (Input.GetKeyDown(KeyCode.F) && this.transform.parent.GetComponent<CharacterController>().isGrounded)
    //        {
    //            Debug.Log("Spotted " + otherObject.name);
    //            //Run whatever interacting with the object would do
    //            otherObject.GetComponent<Interactable>().OnInteract();
    //        }
    //    }
    //}
}
