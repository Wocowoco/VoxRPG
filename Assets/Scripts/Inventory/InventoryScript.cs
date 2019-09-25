using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour {

    [SerializeField]
    private BagSlot bagSlot;
    [SerializeField]
    private WeaponSlot weaponSlot;
    [SerializeField]
    private BagSlot fixedBag;

    [SerializeField]
    private int axeTier = 0;

    //Make it so there's only one inventoryScript
    private static InventoryScript instance;

    public static InventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }

            return instance;
        }
    }

    public BagSlot MyBagSlot
    {
        get
        {
            return bagSlot;
        }
    }

    public WeaponSlot MyWeaponSlot
    {
        get
        {
            return weaponSlot;
        }
    }

    public int InvAxeTier
    {
        get
        {
            return axeTier;
        }

        set
        {
            axeTier = value;
        }
    }

    [SerializeField]
    private Item[] items;

    private void Awake()
    {
        //Set a fake bag for the first 10 inventory slots
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialize(10);
        bag.FixedUse();
        fixedBag.AddItem(bag);
    }

    private void Update()
    {
        //Debug: Spawn bag in inventory
        if (Input.GetKeyDown(KeyCode.K))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(4);
            AddItem(bag);
        }

        //Debug: Spawn bark in inventory
        if (Input.GetKeyDown(KeyCode.J))
        {
            Item item = Instantiate(items[1]);
            AddItem(item);
        }


    }


    public void AddBag(Bag bag)
    {
        //If there's no bag equipped yet, equip one
        if (bagSlot.MyItem == null)
        {
            bagSlot.AddItem(bag);
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        //If there's no weapon equipped yet, equip one
        if (weaponSlot.MyItem == null)
        {
            weaponSlot.AddItem(weapon);
        }
    }


    private bool PlaceInEmptySlot(Item item)
    {
        //Check all slots in the fixed bag
        foreach (SlotScript slot in fixedBag.MyBag.MyBagScript.MySlots)
        {
            //Try to add an item to the fixed bag
            if (fixedBag.MyBag.MyBagScript.AddItem(item))
            {
                //Succesfully added item in new slot
                return true;
            }
        }

        //Check if there is a second bag equipped
        if (bagSlot.MyItem != null)
        {
            //Check all slots in the equiped bag
            foreach (SlotScript slot in bagSlot.MyBag.MyBagScript.MySlots)
            {
                //Try to add an item to the second bag
                if (bagSlot.MyBag.MyBagScript.AddItem(item))
                {
                    //Succesfully added item in new slot
                    return true;
                }
            }
        }

        //If you got here, it failed to place the item in an empty slot
        return false;
    }

    private bool PlaceInStack(Item item)
    {

        //Check all slots in the fixed bag
        foreach (SlotScript slot in fixedBag.MyBag.MyBagScript.MySlots)
        {
            if (slot.StackItem(item))
            {
                //Succesfully stacked item
                return true;
            }
        }

        //Check if there is a second bag equipped
        if (bagSlot.MyItem != null)
        {
            //Check all slots in the equiped bag
            foreach (SlotScript slot in bagSlot.MyBag.MyBagScript.MySlots)
            {
                if (slot.StackItem(item))
                {
                    //Succesfully stacked item
                    return true;
                }
            }
        }

        //Failed to add the item to a stack
        return false;
    }

    //Try adding an item to the inventory
    public bool AddItem(Item item)
    {
        //If the item is stackable, try stacking it
        if (item.MyStackSize > 0)
        {
            //Check if the can be placed on the stack
            if (PlaceInStack(item))
            {
                UpdateTiers(item, true);
                return true;
            }
        }

        //If item couldn't be placed in a stack, place it in a new slot instead
        if (PlaceInEmptySlot(item))
        {
            UpdateTiers(item, true);
            return true;
        }
        else
        {
            return false;
        }
    }

    //Try to remove bag from bagslot
    public bool RemoveBagFromBagSlot(Bag oldBag, int amountOfNewSlots)
    {
        //If the new bag has fewer slots than the old bag, make sure the bag is empty
        if(oldBag.AmountOfSlots > amountOfNewSlots)
        {
            //Check if the old bag is empty (should return true when empty, so invert)
            if(!oldBag.MyBagScript.CheckIfBagIsEmpty())
            {
                //The bag isn't empty, so it shouldn't be removed to prevent loss of items.
                return false;
            }
        }
        //Check all slots in the fixed bag
        foreach (SlotScript slot in fixedBag.MyBag.MyBagScript.MySlots)
        {
            //Try to add the bag in the default bag
            if (fixedBag.MyBag.MyBagScript.AddItem(oldBag))
            {
                //Succesfully added bag in empty fixed slot
                return true;
            }

        }

        //It failed to place equipped bag in the default bag, don't delete bags
        return false;
    }

    public bool RemoveWeaponFromWeaponSlot(Weapon weapon)
    {
        //Try to put the weapon back in your bags
        //Check all slots in the fixed bag
        foreach (SlotScript slot in fixedBag.MyBag.MyBagScript.MySlots)
        {
            //Try to add the bag in the default bag
            if (fixedBag.MyBag.MyBagScript.AddItem(weapon))
            {
                //Succesfully added bag in empty fixed slot
                MyWeaponSlot.RemoveItem(weapon);
                return true;
            }

        }


        //Check all slots in the equipped bag, if one is equipped
        if (bagSlot.MyItem != null)
        {
            //Check all slots in the equiped bag
            foreach (SlotScript slot in bagSlot.MyBag.MyBagScript.MySlots)
            {
                //Try to add an item to the second bag
                if (bagSlot.MyBag.MyBagScript.AddItem(weapon))
                {
                    //Succesfully added item in new slot
                    MyWeaponSlot.RemoveItem(weapon);
                    return true;
                }
            }
        }

        //There is no room in the bags for the weapon, failed to remove weapon
        return false;
    }

    public void UpdateTiers(Item item, bool isBeingAdded)
    {
        //If an item is being added
        if (isBeingAdded)
        {
            //Update axe tiers
            if (item is Axe)
            {

                int newAxeLevel = (item as Axe).MyAxeTier;

                //If the new axe's tier is higher than the current one in the inventory, change it to that one
                if (InvAxeTier < newAxeLevel)
                {
                    InvAxeTier = newAxeLevel;
                }

            }
        }


        //If an item is being removed
        else
        {
            //Update axe tiers
            if (item is Axe)
            {
                //Set axe tier to 0, for now
                InvAxeTier = 0;
                //Check all the slots for the highest tier axe, if any

                //Check all slots in the fixed bag
                foreach (SlotScript slot in fixedBag.MyBag.MyBagScript.MySlots)
                {
                    //Only compare if the item in the slot is an axe
                    if (slot.MyItem is Axe)
                    {
                        int newAxeLevel = (slot.MyItem as Axe).MyAxeTier;

                        //If the new axe's tier is higher than the current one in the inventory, change it to that one
                        if (InvAxeTier < newAxeLevel)
                        {
                            InvAxeTier = newAxeLevel;
                        }
                    }
                }

                //Check if there is a second bag equipped
                if (bagSlot.MyItem != null)
                {
                    //Check all slots in the equiped bag
                    foreach (SlotScript slot in bagSlot.MyBag.MyBagScript.MySlots)
                    {
                        //Only compare if the item in the slot is an axe
                        if (slot.MyItem is Axe)
                        {
                            int newAxeLevel = (slot.MyItem as Axe).MyAxeTier;

                            //If the new axe's tier is higher than the current one in the inventory, change it to that one
                            if (InvAxeTier < newAxeLevel)
                            {
                                InvAxeTier = newAxeLevel;
                            }
                        }
                    }
                }

                //Check the weapon slot
                if (InventoryScript.MyInstance.MyWeaponSlot.MyWeapon is Axe)
                {
                    int newAxeLevel = (InventoryScript.MyInstance.MyWeaponSlot.MyWeapon as Axe).MyAxeTier;

                    //If the new axe's tier is higher than the current one in the inventory, change it to that one
                    if (InvAxeTier < newAxeLevel)
                    {
                        InvAxeTier = newAxeLevel;
                    }
                }

            }
        }

        Debug.Log("Axe Tier: " + InvAxeTier);
    }

}
