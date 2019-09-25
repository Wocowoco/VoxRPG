using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Weapon", order = 1)]
public class Weapon : Item
{
    [Header("Weapon Properties")]
    [SerializeField]
    private int minimumDamage = 0;
    [SerializeField]
    private int maximumDamage = 0;
    [SerializeField]
    private DamageType.Type damageType;
    [SerializeField]
    private float attackSpeed = 0;

    [Header("Weapon Stats")]
    [SerializeField]
    private int strength;

    public int MinDamage
    {
        get
        {
            return minimumDamage;
        }
    }

    public int MaxDamage
    {
        get
        {
            return maximumDamage;
        }
    }

    public float AttackSpeed
    {
        get
        {
            return attackSpeed;
        }
    }

    public int Strength
    {
        get
        {
            return strength;
        }
    }

    public DamageType.Type MyDamageType
    {
        get
        {
            return damageType;
        }
    }



    override public void Use()
    {
        //Check if the weapon slot is still empty before adding it
        if (InventoryScript.MyInstance.MyWeaponSlot.MyWeapon == null)
        {
            //Remove item from inventory
            Remove();

            //Tell the Inventory that we are equipping it in the weapon slot
            InventoryScript.MyInstance.AddWeapon(this);

        }
        else //There is already a weapon equipped, try to swap it
        {
            //Get slot current weapon is in
            SlotScript slot = this.MySlot;
            Weapon oldWeapon = InventoryScript.MyInstance.MyWeaponSlot.MyWeapon;
            Weapon newWeapon = this;

            InventoryScript.MyInstance.MyWeaponSlot.RemoveItem(oldWeapon);
            InventoryScript.MyInstance.MyWeaponSlot.AddItem(newWeapon);

            slot.RemoveItem(newWeapon);
            slot.AddItem(oldWeapon);
        }
    }
}
