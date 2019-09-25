using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private int maxHp = 0;
    private int currentHp = 0;
    [Header("Offensive Properties")]
    [Header("Defensive Properties (%)")]
    [SerializeField]
    [Range(50, 200)]
    private int def = 100;
    [SerializeField]
    [Range(50, 200)]
    private int firedef = 100;
    [SerializeField]
    [Range(50, 200)]
    private int waterdef = 100;
    [SerializeField]
    [Range(50, 200)]
    private int earthdef = 100;
    [SerializeField]
    [Range(50, 200)]
    private int airdef = 100;



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

    public void TakeDamage(int amountOfDamage, DamageType.Type damageType = DamageType.Type.normal ,bool isCrit = false)
    {
        if (isCrit)
        {
            //Increase damage with the player's modifier
            amountOfDamage = (int)(amountOfDamage * PlayerStats.MyInstance.GetCritModifier());
        }

        //Take the enemy's resists in to account
        switch (damageType)
        {
            case DamageType.Type.normal:
                amountOfDamage = (int)(amountOfDamage / (def / 100.0f));
                break;
            case DamageType.Type.fire:
                amountOfDamage = (int)(amountOfDamage / (firedef / 100.0f));
                break;
            case DamageType.Type.water:
                amountOfDamage = (int)(amountOfDamage / (waterdef / 100.0f));
                break;
            case DamageType.Type.earth:
                amountOfDamage = (int)(amountOfDamage / (earthdef / 100.0f));
                break;
            case DamageType.Type.air:
                amountOfDamage = (int)(amountOfDamage / (airdef / 100.0f));
                break;
        }

        currentHp -= amountOfDamage;
        //Show damage number on screen 
        if (isCrit)
        {
            DamageTextController.CreateCritText(amountOfDamage.ToString(), this.gameObject);
        }
        else
        {
            DamageTextController.CreateDamageText(amountOfDamage.ToString(), this.gameObject);
        }


        //Check if the enemy is still alive, if it isn't kill it
        if (currentHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
