using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSlot : SlotScript {

    [SerializeField]
    private GameObject PlayerHand;

    public Weapon MyWeapon
    {
        get
        {
            return (Weapon)MyItem;
        }

    }

    override public void RemoveItem(Item item)
    {
        //Only remove items if there is an item
        if (items.Count > 0)
        {
            items.Pop();
        }

        //If this was the last item, remove the item's icon from the slot
        if (items.Count == 0)
        {
            icon.color = new Color(0, 0, 0, 0);
        }

        //Remove weapon from the player's hand
        foreach (Transform child in PlayerHand.transform)
        {
            Destroy(child.gameObject);
        }
        //Update player's stats by removing this item's bonuses
        PlayerStats.MyInstance.RemoveFromPlayerStats(item);
        //Update tool tiers when removing item
        InventoryScript.MyInstance.UpdateTiers(item, false);
    }


    override public void OnPointerClick(PointerEventData eventData)
    {
        //Checking EventData

        //Check if leftclick
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //If double leftclick
            if (eventData.clickCount >= 2)
            {
                UseItem();
            }
        }

        //Check if rightclick
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }

    override public bool AddItem(Item item)
    {
        //Add item
        items.Push(item);

        //If the item is stackable, show it's stack count
        if (item.MyStackSize > 1)
        {
            StackSize.text = items.Count.ToString();
        }

        //Set the image of the slot to the image of the item and make it visible
        icon.sprite = item.MyIcon;
        icon.color = Color.white;

        //Let the item know in which slot it is
        item.MySlot = this;

        //Update the player's stats
        PlayerStats.MyInstance.AddToPlayerStats(item);
        //Update tiers
        //Update tool tiers when removing item
        InventoryScript.MyInstance.UpdateTiers(item, true);

        //Visually show the item in the player's hand
        Instantiate(MyWeapon.HandObject, PlayerHand.transform);

        //Return true if the item was succesfully added
        return true;
    }

    override public void UseItem()
    {
        //If the weaponslot is not empty
        if (!IsEmpty)
        {
            //If the weapon can be added to the inventory, remove it, otherwise keep it equipped.
            InventoryScript.MyInstance.RemoveWeaponFromWeaponSlot(MyWeapon);

        }
    }
}
