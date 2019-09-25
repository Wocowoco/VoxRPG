using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    //Make it so there's only one Playerstats
    private static PlayerStats instance;

    public static PlayerStats MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerStats>();
            }

            return instance;
        }
    }

    //Basic stats
    private int health = 10;
    private int wisdom = 0;
    private float movespeed = 0.0f;
    private float luck = 0;


    //Offensive stats
    [SerializeField]
    private int strength = 0;
    private int dexterity = 0;
    private int intelligence = 0;

    private int nrmldmg = 100;
    private int firedmg = 100;
    private int waterdmg = 100;
    private int earthdmg = 100;
    private int airdmg = 100;

    private float critChance = 25.0f;
    private float critModifier = 1.50f;

    //Defensive stats 
    private int def = 100;
    private int firedef = 100;
    private int waterdef = 100;
    private int earthdef = 100;
    private int airdef = 100;


    public float GetCritChance()
    {
        return critChance;
    }

    public float GetCritModifier()
    {
        return critModifier;
    }

    public int GetStrength()
    {
        return strength;
    }

    public void AddToPlayerStats(Item item)
    {
        //Check if the added item is a weapon
        if (item is Weapon)
        {
            strength += (item as Weapon).Strength;
        }
    }

    public void RemoveFromPlayerStats(Item item)
    {
        //Check if the added item is a weapon
        if (item is Weapon)
        {
            strength -= (item as Weapon).Strength;
        }
    }

    public float GetDamageBonus(DamageType.Type damageType)
    {
        switch (damageType)
        {
            case DamageType.Type.normal:
                return (nrmldmg/100.0f);
            case DamageType.Type.fire:
                return (firedmg / 100.0f);
            case DamageType.Type.water:
                return (waterdmg / 100.0f);
            case DamageType.Type.earth:
                return (earthdmg / 100.0f);
            case DamageType.Type.air:
                return (airdmg / 100.0f);
            default:
                return 1.0f;
        }
    }

}
