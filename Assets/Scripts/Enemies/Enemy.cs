using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 0;
    private int currentHp = 0;



    public int MyMaxHP
    {
        get
        {
            return maxHp;
        }
    }

    public int MyCurrentHP
    {
        get
        {
            return currentHp;
        }
    }

    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int amountOfDamage, bool isCrit = false)
    {



        if (isCrit)
        {
            //Reduce enemy HP by amount of damage taken
            amountOfDamage = (int)(amountOfDamage * PlayerStats.MyInstance.GetCritModifier());
            currentHp -= amountOfDamage;
            //Show damage number on screen 
            DamageTextController.CreateCritText(amountOfDamage.ToString(), this.gameObject);
        }
        else
        {        
            //Reduce enemy HP by amount of damage taken
            currentHp -= amountOfDamage;
            //Show damage number on screen 
            DamageTextController.CreateDamageText(amountOfDamage.ToString(), this.gameObject);
        }


        //Check if the enemy is still alive, if it isn't kill it
        if(currentHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
