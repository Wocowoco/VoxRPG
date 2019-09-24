using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

    private Collider[] hitCollider;
    private Collider[] attackCollider;
    private GameManage gameManager;
    private bool isAllowedToUpdate = true;
    private bool isReadyToAttack = true;
    private float attackCooldDown = 0.75f;
    private float currentAttackCD = 0.0f;


    public bool IsAllowedToUpdate
    {
        set
        {
            isAllowedToUpdate = value;
        }
    }
    // Use this for initialization
    void Start () {
        gameManager = GameManage.MyInstance;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if attack is on CD
        if (!isReadyToAttack)
        {
            //If it is on CD, add time every frame
            currentAttackCD += Time.deltaTime;

            //If the time is higher or equal to the CD, make the attack available again.
            if (currentAttackCD >= attackCooldDown)
            {
                isReadyToAttack = true;
                currentAttackCD = 0.0f;
            }
        }

        //Only update and take input when allowed to (not in menu)
        if (isAllowedToUpdate)
        {
            //--------------//
            //  USE ACTION  //
            //--------------//
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

            //--------------//
            //   ATTACK     //
            //--------------//
            //Only attack when you attack is off cooldown
            if (isReadyToAttack && Input.GetMouseButtonDown(0))
            {
                //Player just attacked, make it go on cooldown
                isReadyToAttack = false;


                //CODE TO CHECK FOR EQUIPPED WEAPON

                //Turn player to face towards where he is aiming
                gameManager.FacePlayerTowardsAim(0.75f);

                //If no weapon equipped, do a melee swing (punch)
                attackCollider = Physics.OverlapBox(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), new Vector3(1,1,1));
                //Set animation to punching if standing still, else set animation to walking punch
                if(GetComponentInParent<PlayerMovement>().PlayerAnimations.GetFloat("Speed") <= 0.1f)
                {
                    GetComponentInParent<PlayerMovement>().PlayerAnimations.Play("Player_Punch");
                }
                else
                {
                    GetComponentInParent<PlayerMovement>().PlayerAnimations.Play("Player_WalkingPunch");
                }
               
                //Deal damage to all enemies hit
                int targetHit = 0;
                while (targetHit < attackCollider.Length)
                {
                    if (attackCollider[targetHit].GetComponent<Enemy>() != null)
                    {
                        Debug.Log("Dealt damage to an enemy.");
                        attackCollider[targetHit].GetComponent<Enemy>().TakeDamage(1);
                    }

                    targetHit++;
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
