using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

    private Collider[] hitCollider;
    private Collider[] attackCollider;
    private GameManage gameManager;
    private bool isAllowedToUpdate = true;
    private bool isReadyToAttack = true;
    private float attackCooldDown = 0.0f;
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
            if (Input.GetKeyDown(KeyCode.F) && GetComponent<CharacterController>().isGrounded)
            {
                //Get area to look if it hit
                Transform hitZone = GameObject.Find("ActionZone").transform;
                hitCollider = Physics.OverlapBox(hitZone.position, new Vector3(0.4f, 0.5f, 0.375f), hitZone.rotation);
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

                //--------------------//
                // ATTACK WITH WEAPON //
                //--------------------//
                if (InventoryScript.MyInstance.MyWeaponSlot.MyWeapon != null)
                {
                    //
                    //Make the attack go on cooldown for the weapon attack speed
                    attackCooldDown = InventoryScript.MyInstance.MyWeaponSlot.MyWeapon.AttackSpeed;
                    //Turn player to face towards where he is aiming, lock it for the duration of the attack
                    gameManager.FacePlayerTowardsAim(attackCooldDown + 0.2f);

                    attackCollider = Physics.OverlapBox(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), new Vector3(1, 1, 1));
                    
                    //Deal damage to all enemies hit
                    int targetHit = 0;
                    while (targetHit < attackCollider.Length)
                    {
                        if (attackCollider[targetHit].GetComponent<Enemy>() != null)
                        {
                            //Weapon damage
                            int damage = CalculatePlayerDamage(InventoryScript.MyInstance.MyWeaponSlot.MyWeapon.MinDamage, 
                                InventoryScript.MyInstance.MyWeaponSlot.MyWeapon.MaxDamage, 
                                InventoryScript.MyInstance.MyWeaponSlot.MyWeapon.MyDamageType);

                            //Check if the player crits
                            float critChance = PlayerStats.MyInstance.GetCritChance();
                            float critCheck = Random.Range(0.0f, 100.0f);
                            //If check is below critChance, than the player scored a crital hit
                            if (critCheck < critChance)
                            {
                                //It is a crit
                                attackCollider[targetHit].GetComponent<Enemy>().TakeDamage(damage, InventoryScript.MyInstance.MyWeaponSlot.MyWeapon.MyDamageType, true);
                            }
                            else //Not a crit
                            {
                                attackCollider[targetHit].GetComponent<Enemy>().TakeDamage(damage, InventoryScript.MyInstance.MyWeaponSlot.MyWeapon.MyDamageType);
                            }
                        }
                        targetHit++;
                    }

                    //Do the correct animation
                    if (InventoryScript.MyInstance.MyWeaponSlot.MyWeapon is Axe)
                    {
                        GetComponentInParent<PlayerMovement>().PlayerAnimations.Play("Player_AxeSwing");
                    }
                }
                //-----//
                //PUNCH//
                //-----//
                else
                {
                    //If no weapon equipped, do a melee swing (punch)
                    //Make the attack go on cooldown for the weapon attack speed
                    attackCooldDown = 0.75f;
                    //Turn player to face towards where he is aiming
                    gameManager.FacePlayerTowardsAim(0.5f);
                    attackCollider = Physics.OverlapBox(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), new Vector3(0.5f, 0.5f, 0.5f));
                    //Deal damage to all enemies hit
                    int targetHit = 0;
                    while (targetHit < attackCollider.Length)
                    {
                        //Calculate Damage

                        if (attackCollider[targetHit].GetComponent<Enemy>() != null)
                        {
                            //Punch damage
                            int damage = CalculatePlayerDamage(1);

                            //Check if the player crits
                            float critChance = PlayerStats.MyInstance.GetCritChance();
                            float critCheck = Random.Range(0.0f, 100.0f);
                            //If check is below critChance, than the player scored a crital hit
                            if (critCheck < critChance)
                            {
                                attackCollider[targetHit].GetComponent<Enemy>().TakeDamage(damage, DamageType.Type.normal, true);
                            }
                            else //Not a crit
                            {
                                attackCollider[targetHit].GetComponent<Enemy>().TakeDamage(damage);
                            }
                        }
                        targetHit++;
                    }
                    //Do a punching animation once
                    GetComponentInParent<PlayerMovement>().PlayerAnimations.Play("Player_Punch");
                }
               
                
            }
        }
    }

    private int CalculatePlayerDamage(int lowerAmount, int upperAmount = 0, DamageType.Type damageType = DamageType.Type.normal)
    {
        int minDmg = lowerAmount;
        int maxDmg = upperAmount;
        //If both amounts are equal, the weapon needs to deal fixed damage
        if (lowerAmount == upperAmount)
        {
            upperAmount = 0;
        }
        float playerDamageBonus = PlayerStats.MyInstance.GetDamageBonus(damageType);
        
        //Modify damage with player's stats
        int str = PlayerStats.MyInstance.GetStrength();


        if (upperAmount != 0) //There is a upper and lower limit to this attack
        {
            //Calc max dmg
            maxDmg += (int)((1.0f / 1.875f) * str + (Mathf.Pow(str, 2.0f)) * (1.0f/2375.0f) * playerDamageBonus);

            //Calc min dmg
            minDmg += (int)((1.0f / 2.25f) * str + (Mathf.Pow(str, 2.0f)) * (1.0f / 2375.0f) * playerDamageBonus);

        }
        else //This attack deals fixed damage
        {
            return minDmg += (int)((1.0f / 1.875f) * str + (Mathf.Pow(str, 2.0f)) * (1.0f / 2375.0f) * playerDamageBonus);
        }

        //Return a random damage number between min and max damage.
        return (int) Random.Range(minDmg,maxDmg+1);

    }
}
