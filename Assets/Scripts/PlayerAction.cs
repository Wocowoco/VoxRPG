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

                //-----//
                //PUNCH//
                //-----//
                //If no weapon equipped, do a melee swing (punch)
                //Turn player to face towards where he is aiming
                gameManager.FacePlayerTowardsAim(0.75f);
                attackCollider = Physics.OverlapBox(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), new Vector3(1,1,1));
                //Deal damage to all enemies hit
                int targetHit = 0;
                while (targetHit < attackCollider.Length)
                {
                    //Calculate Damage
                    
                    if (attackCollider[targetHit].GetComponent<Enemy>() != null)
                    {
                        //Punch damage
                        int damage = CalculatePlayerDamage(1,2);

                        //Check if the player crits
                        float critChance = PlayerStats.MyInstance.GetCritChance();
                        float critCheck = Random.Range(0.0f, 100.0f);
                        //If check is below critChance, than the player scored a crital hit
                        if (critCheck < critChance)
                        {
                            attackCollider[targetHit].GetComponent<Enemy>().TakeDamage(damage, true);
                        }
                        else //Not a crit
                        {
                            attackCollider[targetHit].GetComponent<Enemy>().TakeDamage(damage);
                        }
                    }
                    targetHit++;
                }


                //Set animation to punching if standing still, else set animation to walking punch
                if (GetComponentInParent<PlayerMovement>().PlayerAnimations.GetFloat("Speed") <= 0.1f)
                {
                    GetComponentInParent<PlayerMovement>().PlayerAnimations.Play("Player_Punch");
                }
                else
                {
                    GetComponentInParent<PlayerMovement>().PlayerAnimations.Play("Player_WalkingPunch");
                }
               
                
            }
        }
    }

    private int CalculatePlayerDamage(int lowerAmount, int upperAmount = 0)
    {
        int minDmg = lowerAmount;
        int maxDmg = upperAmount;

        //Modify damage with player's stats
        int str = PlayerStats.MyInstance.GetStrength();

        if (upperAmount != 0) //There is a upper and lower limit to this attack
        {
            //Calc max dmg
            maxDmg += (int)((1.0f / 1.875f) * str + (Mathf.Pow(str, 2.0f)) * (1.0f/2375.0f));

            //Calc min dmg
            minDmg += (int)((1.0f / 2.25f) * str + (Mathf.Pow(str, 2.0f)) * (1.0f / 2375.0f));

            //Debug.Log("Min DMG: " + minDmg + ", Max DMG: " + maxDmg);
        }
        else //This attack deals fixed damage
        {
            return minDmg += (int)((1.0f / 1.875f) * str + (Mathf.Pow(str, 2.0f)) * (1.0f / 2375.0f));
        }

        //Return a random damage number between min and max damage.
        return (int) Random.Range(minDmg,maxDmg+1);

    }
}
