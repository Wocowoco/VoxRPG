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
    private int strength = 1000;
    private int dexterity = 0;
    private int intelligence = 0;

    private int firedmg = 0;
    private int waterdmg = 0;
    private int earthdmg = 0;
    private int airdmg = 0;

    private float critChance = 25.0f;
    private float critModifier = 1.50f;

    //Defensive stats
    private int defence = 0;
    private int magicdef = 0;

    private int firedef = 0;
    private int waterdef = 0;
    private int earthdef = 0;
    private int airdef = 0;


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

}
